using LM.Application.Helpers;
using LM.Application.Interfaces.Persistence;
using LM.Application.Interfaces.Services;
using LM.Application.Interfaces.Utilities;
using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Entities;
using LM.Domain.Enums.EnumExtension;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Response;
using LM.DTOs.Response.BookVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Services.Implementations
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IContextAccessor _contextAccessor;
        private readonly IStoreManager<Book> _book;
        private readonly IStoreManager<BookHistory> _bookHistory;
        private readonly UserManager<LMUser> _userManager;

        public TransactionsService(
            IContextAccessor contextAccessor,
            IStoreManager<Book> book,
            IStoreManager<BookHistory> bookHistory,
            UserManager<LMUser> userManager)
        {
            this._contextAccessor = contextAccessor;
            this._book = book;
            this._bookHistory = bookHistory;
            this._userManager = userManager;
        }

        public async Task<ResultModel<IssueBookVm>> IssueBook(Guid bookID)
        {
            var response = new IssueBookVm();

            var userId = _contextAccessor.GetCurrentUserId();

            var book = await GetBookByID(bookID);

            //if book in Library
            if (book is null)
                return new ResultModel<IssueBookVm>($"Book with ID: {bookID} Not Found");

            //if available
            if (book.Quantity < 1)
                return new ResultModel<IssueBookVm>($"Book with ID: {bookID} Not Available to Issued Out (Out Of Stock)");

            //check if the customer currently have a copy of the said book
            var bookHistory = await IfBookWithCustomer(bookID, userId.ToString());

            if (bookHistory is not null)
                return new ResultModel<IssueBookVm>($"Book with ID: {bookID} In your Possession");

            var issueBook = new BookHistory()
            {
                ID = Guid.NewGuid(),
                BookId = bookID,
                LMUserId = userId.ToString(),
                BorrowStatus = Domain.Enums.BookCustomerStatus.IssuedOut,
                CreationTime = DateTime.Now,
                IssuedDate = DateTime.Now
            };

            await _bookHistory.DataStore.Add(issueBook);
            await _bookHistory.SaveChanges();

            book.Quantity--;

            _book.DataStore.Update(book);
            await _book.SaveChanges();

            response.BookDetails = book;
            response.ExpectedReturnDate = DateTimeExtention.AddBusinessDays(DateTime.Now, 14).ToLongDateString();

            return new ResultModel<IssueBookVm> { Data = response, Message = "Successful!" };
        }

        public async Task<ResultModel<returnBookVm>> ReturnBook(Guid bookID)
        {
            var result = new returnBookVm();

            var userId = _contextAccessor.GetCurrentUserId();

            var book = await GetBookByID(bookID);

            //if book is a Library book
            if (book is null)
                return new ResultModel<returnBookVm>($"Book with ID: {bookID} Not Found");

            //check if the customer currently have a copy of the said book
            var bookHistory = await IfBookWithCustomer(bookID, userId.ToString());

            if (bookHistory is null)
                return new ResultModel<returnBookVm>($"Book with ID: {bookID} Not in your Possession");

            bookHistory.BorrowStatus = Domain.Enums.BookCustomerStatus.Returned;
            bookHistory.ReturnedDate = DateTime.Now;
            
            _bookHistory.DataStore.Update(bookHistory);
            await _bookHistory.SaveChanges();

            book.Quantity++;

            _book.DataStore.Update(book);
            await _bookHistory.SaveChanges();

            var numberOfworkingDays = DateTimeExtention.GetWorkingDays(bookHistory.IssuedDate, DateTime.Now);

            result.NumberOfDays = $"Lasted for {numberOfworkingDays} Working Days";
            result.BorrowedDate = bookHistory.IssuedDate.ToLongDateString();
            result.ReturnedDate = DateTime.Now.ToLongDateString();
            result.BookDetails = book;
            result.ExpectedReturnDate = DateTimeExtention.AddBusinessDays(bookHistory.IssuedDate, 14).ToLongDateString();
            result.Charges = (numberOfworkingDays > 14) ? $" {_ = ((numberOfworkingDays - 14) * 5000)} NGN " : $" {0M} NGN ";

            return new ResultModel<returnBookVm> { Data = result, Message = "Thanks!" };
        }

        public async Task<ResultModel<PagedList<BookHistoryVM>>> UserOverAllHistory(pagiSearchVm model)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var query = GetUserBookHistory(userId.ToString());

            return await SearchBooksInv(query, model);
        }

        public async Task<ResultModel<PagedList<BookHistoryVM>>> UserOverAllHistory(pagiSearchVm model, Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return new ResultModel<PagedList<BookHistoryVM>>($"User with this User ID {userId} doesn't exixts");

            var query = GetUserBookHistory(userId.ToString());

            return await SearchBooksInv(query, model);
        }

        private Task<Book> GetBookByID(Guid ID)
        {
            return _book.DataStore.GetAllQuery().Where(x => x.ID == ID).FirstOrDefaultAsync();
        }

        private Task<BookHistory> IfBookWithCustomer(Guid bookId, string userId)
        {
            return _bookHistory.DataStore.GetAllQuery()
                .Where(x => x.BookId == bookId && x.LMUserId == userId && x.BorrowStatus == Domain.Enums.BookCustomerStatus.IssuedOut).FirstOrDefaultAsync();
        }

        private IQueryable<BookHistory> GetUserBookHistory(string userId)
        {
            return _bookHistory.DataStore.GetAllQuery().Include(x => x.Books).Include(x => x.LMUsers)
                .Where(x => x.LMUserId == userId);
        }

        private async Task<ResultModel<PagedList<BookHistoryVM>>> SearchBooksInv(IQueryable<BookHistory> query, pagiSearchVm model)
        {
            var bookhistories = await query.OrderByDescending(x => x.BorrowStatus == Domain.Enums.BookCustomerStatus.IssuedOut)
                .ToPagedListAsync(model.PageIndex, model.PageSize);

            var bookhisvms = bookhistories.Select(x => new BookHistoryVM
            {
                BookDetails = x.Books,
                BookStatus = x.BorrowStatus.GetDescription(),
                IssuedDate = x.IssuedDate.ToLongDateString(),
                ReturnedDate = (x.BorrowStatus == Domain.Enums.BookCustomerStatus.IssuedOut) ? "Not Returned" : x.ReturnedDate.ToLongDateString(),
                UserFullName = $"{x.LMUsers.FirstName} {x.LMUsers.LastName}"
            }).ToList();

            var data = new PagedList<BookHistoryVM>(bookhisvms, model.PageIndex, model.PageSize, bookhistories.TotalItemCount);

            return new ResultModel<PagedList<BookHistoryVM>> { Data = data, Message = $"Found {bookhisvms.Count} Books." };
        }
    }
}

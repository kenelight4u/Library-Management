using LM.Application.Interfaces.Persistence;
using LM.Application.Interfaces.Services;
using LM.Application.Interfaces.Utilities;
using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Entities;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Request.BookDTO;
using LM.DTOs.Request.BookGenreDTO;
using LM.DTOs.Response.BookGenresVM;
using LM.DTOs.Response.BookVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Services.Implementations
{
    public class BookService : IBookService
    {
        /// <summary>
        /// The Context Accessor
        /// </summary>
        private readonly IContextAccessor _contextAccessor;
        /// <summary>
        /// The Book Genres Repository
        /// </summary>
        private readonly IStoreManager<BookGenres> _bookGenre;
        /// <summary>
        /// The Book Repository
        /// </summary>
        private readonly IStoreManager<Book> _book;
        /// <summary>
        /// The Book Inventory repository
        /// </summary>
        private readonly IStoreManager<BookInventory> _bookInv;
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="contextAccessor"></param>
        /// <param name="bookInv"></param>
        /// <param name="book"></param>
        /// <param name="bookGenre"></param>
        /// <param name="unitOfWork"></param>
        public BookService(
            IContextAccessor contextAccessor,
            IStoreManager<BookInventory> bookInv,
            IStoreManager<Book> book,
            IStoreManager<BookGenres> bookGenre,
            IUnitOfWork unitOfWork)
        {
            this._book = book;
            this._bookInv = bookInv;
            this._contextAccessor = contextAccessor;
            this._bookGenre = bookGenre;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Create a Book
        /// </summary>
        /// <param name="bookGDTO"></param>
        /// <returns></returns>
        public async Task<ResultModel<Guid>> AddNewBook(BookDTO bookGDTO)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var bookGenre = await GetBookGenreByID(bookGDTO.BookGenresId);

            if (bookGenre is null)
                return new ResultModel<Guid>($"Book Genre with ID: {bookGenre.ID} Not Found");
               
            var book = await GetBookByISBN(bookGDTO.ISBN);

            if (book is not null)
                return new ResultModel<Guid>($"Book with ISBN: {bookGDTO.ISBN} Exists");

            var newBook = new Book
            {
                ID = Guid.NewGuid(),
                Title = bookGDTO.Title,
                Author = bookGDTO.Author,
                BookGenresId = bookGenre.ID,
                CreationTime = DateTime.Now,
                Description = bookGDTO.Description,
                CreatorUserId = userId.ToString(),
                ISBN = bookGDTO.ISBN,
                Quantity = bookGDTO.Quantity
            };

            await _book.DataStore.Add(newBook);
            //await _bookGenre.SaveChanges();
            

            var newBookInv = new BookInventory
            {
                ID = Guid.NewGuid(),
                Quantity = bookGDTO.Quantity,
                CreationTime = DateTime.Now,
                BookId = newBook.ID,
                CreatorUserId = userId.ToString()
            };

            await _bookInv.DataStore.Add(newBookInv);
            //await _bookInv.SaveChanges();

            await _unitOfWork.SaveChangesAsync();

            return new ResultModel<Guid> { Data = newBook.ID, Message = "Book Created Successfully" };
        }

        public async Task<ResultModel<string>> DeleteABook(Guid ID)
        {
            var isDeleted = await _book.DataStore.Delete(ID);

            //await _book.SaveChanges();
            await _unitOfWork.SaveChangesAsync();

            if (isDeleted)
                return new ResultModel<string> { Data = "Success", Message = $"Book with ID: {ID} Deleted Successful" };

            return new ResultModel<string>
            {
                Data = "Failed",
                Message = $"Book with ID: {ID} Not Found",
                ValidationErrors = new List<ValidationResult>
                    {new ValidationResult($"Book with ID: {ID} Not Found", null)}
            };
        }

        public async Task<ResultModel<string>> EditABook(EditBookDTO bookGDTO)
        {
            var book = await GetBookByID(bookGDTO.ID);

            if (book is null)
                return new ResultModel<string>($"Book with ID: {bookGDTO.ID} Not Found");

            if (!string.IsNullOrWhiteSpace(bookGDTO.ISBN))
            {
                var bookIsbn = await GetBookByISBN(bookGDTO.ISBN);

                if (bookIsbn is not null)
                    return new ResultModel<string>($"Book with ISBN: {bookGDTO.ISBN} already Exist");
            }

            book.LastModificationTime = DateTime.Now;
            book.Title = bookGDTO.Title is null ? book.Title : bookGDTO.Title;
            book.ISBN = bookGDTO.ISBN is null ? book.ISBN : bookGDTO.ISBN;
            book.Author = bookGDTO.Author is null ? book.Author : bookGDTO.Author;
            book.Description = bookGDTO.Description is null ? book.Description : bookGDTO.Description;

            _book.DataStore.Update(book);
            //await _book.SaveChanges();
            await _unitOfWork.SaveChangesAsync();

            return new ResultModel<string> { Data = "Success", Message = "Book Updated Successfully" };
        }

        public async Task<ResultModel<BookVM>> GetABookByID(Guid ID)
        {
            var book = await GetBookByID(ID);

            if (book is null)
                return new ResultModel<BookVM>($"Book with ID: {ID} Not Found");

            return new ResultModel<BookVM> { Data = book, Message = "Success" };
        }

        public async Task<ResultModel<BookVM>> GetABookByISBN(string isbn)
        {
            var book = await GetBookByISBN(isbn);

            if (book is null)
                return new ResultModel<BookVM>($"Book with ISBN: {isbn} Not Found");

            return new ResultModel<BookVM> { Data = book, Message = "Success" };     
        }

        public async Task<ResultModel<PagedList<BookVM>>> GetAllBooks(SearchVM model)
        {
            var query = _book.DataStore.GetAllQuery();

            return await SearchBooks(query, model);
        }

        public async Task<ResultModel<PagedList<BookInventoryVM>>> GetAllBooksInventory(pagiSearchVm model)
        {
            var query = _bookInv.DataStore.GetAllQuery().Include(x => x.Books);

            return await SearchBooksInv(query, model);
        }

        public async Task<ResultModel<string>> UpdateInventory(BookInventoryUpdateDTO bookInvDTO)
        {
            var book = await GetBookByID(bookInvDTO.BookID);

            if (book is null)
                return new ResultModel<string>($"Book with ID: {bookInvDTO.BookID} Not Found");

            var bookInv = await GetBookInventoryByID(bookInvDTO.BookID);

            if (bookInv is null)
                return new ResultModel<string>($"Book with ID: {bookInvDTO.BookID} Not Found");

            book.Quantity += bookInvDTO.Quantity;

            bookInv.Quantity += bookInvDTO.Quantity;

            _book.DataStore.Update(book);
           // await _book.SaveChanges();

            _bookInv.DataStore.Update(bookInv);
           // await _bookInv.SaveChanges();

            await _unitOfWork.SaveChangesAsync();

            return new ResultModel<string> { Data = "Success", Message = "Book Quantity Updated Successfully" };
        }

        private Task<Book> GetBookByISBN(string isbn)
        {
            return _book.DataStore.GetAllQuery().Where(x => x.ISBN == isbn).FirstOrDefaultAsync();
        }

        private Task<Book> GetBookByID(Guid ID)
        {
            return _book.DataStore.GetAllQuery().Where(x => x.ID == ID).FirstOrDefaultAsync();
        }

        private Task<BookGenres> GetBookGenreByID(Guid ID)
        {
            return _bookGenre.DataStore.GetAllQuery().Where(x => x.ID == ID).FirstOrDefaultAsync();
        }

        private Task<BookInventory> GetBookInventoryByID(Guid ID)
        {
            return _bookInv.DataStore.GetAllQuery().Where(x => x.BookId == ID).FirstOrDefaultAsync();
        }

        private async Task<ResultModel<PagedList<BookVM>>> SearchBooks(IQueryable<Book> query, SearchVM model)
        {
            if (model.Filter is not null)
                query = BuildQueryFilter(query, model.Filter);

            var books = await query.OrderByDescending(x => x.CreationTime)
                .ToPagedListAsync(model.PageIndex, model.PageSize);

            var bookVms = books.Select(x => (BookVM)x).ToList();

            var data = new PagedList<BookVM>(bookVms, model.PageIndex, model.PageSize, books.TotalItemCount);

            return new ResultModel<PagedList<BookVM>> { Data = data, Message = $"Found {books.Count} Books." };
        }

        private IQueryable<Book> BuildQueryFilter(IQueryable<Book> query, Filter filter)
        {

            switch (filter.FilterColumn)
            {
                case "ISBN":
                    query = query.Where(x => x.ISBN.ToLower().Contains(filter.Keyword.ToLower()));
                    break;
                case "Title":
                    query = query.Where(x => x.Title.ToLower().Contains(filter.Keyword.ToLower()));
                    break;
                case "Author":
                    query = query.Where(x => x.Author.ToLower().Contains(filter.Keyword.ToLower()));
                    break;
                default:
                    break;
            }

            return query;
        }

        private async Task<ResultModel<PagedList<BookInventoryVM>>> SearchBooksInv(IQueryable<BookInventory> query, pagiSearchVm model)
        {
            var bookInvs = await query.OrderByDescending(x => x.CreationTime)
                .ToPagedListAsync(model.PageIndex, model.PageSize);

            var bookVms = bookInvs.Select(x => (BookInventoryVM)x).ToList();

            var data = new PagedList<BookInventoryVM>(bookVms, model.PageIndex, model.PageSize, bookInvs.TotalItemCount);

            return new ResultModel<PagedList<BookInventoryVM>> { Data = data, Message = $"Found {bookInvs.Count} Books." };
        }
    }
}

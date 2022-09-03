using LM.Application.Interfaces.Persistence;
using LM.Application.Interfaces.Services;
using LM.Application.Interfaces.Utilities;
using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Entities;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Request.BookGenreDTO;
using LM.DTOs.Response.BookGenresVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LM.Services.Implementations
{
    public class BookGenreService : IBookGenresService
    {
        private readonly IContextAccessor _contextAccessor;
        private readonly IStoreManager<BookGenres> _bookGenre;

        public BookGenreService(
            IStoreManager<BookGenres> bookGenre,
            IContextAccessor contextAccessor)
        {
            this._bookGenre = bookGenre;
            this._contextAccessor = contextAccessor;
        }

        public async Task<ResultModel<string>> AddBookGenre(BookGenresDTO bookGenresDTO)
        {
            var userId = _contextAccessor.GetCurrentUserId();

            var bookGenre = await _bookGenre.DataStore.GetAllQuery().Where(x => x.Name == bookGenresDTO.Name).FirstOrDefaultAsync();
            
            if (bookGenre is not null)
                return new ResultModel<string>
                {
                    Data = "Failed",
                    Message = $"Book Genre with Name: {bookGenre.Name} Exist",
                    ValidationErrors = new List<ValidationResult>
                    {new ValidationResult($"Book Genre with Name: {bookGenre.Name} Exist", null)}
                };

            var newBookGenre = new BookGenres()
            {
                Name = bookGenresDTO.Name,
                CreationTime = DateTime.Now,
                Description = bookGenresDTO.Description,
                CreatorUserId = userId.ToString(),
                ID = Guid.NewGuid()
            };

            await _bookGenre.DataStore.Add(newBookGenre);
            await _bookGenre.SaveChanges();

            return new ResultModel<string> { Data = "Success", Message = "Book Genre Created Successfully" };
        }

        public async Task<ResultModel<string>> DeleteABookGenre(Guid ID)
        {
            var isDeleted = await _bookGenre.DataStore.Delete(ID);

            await _bookGenre.SaveChanges();

            if (isDeleted)
                return new ResultModel<string> { Data = "Success", Message = $"Book Genre with ID: {ID} Deleted Successful" };

            return new ResultModel<string>
            {
                Data = "Failed",
                Message = $"Book Genre with ID: {ID} Not Found",
                ValidationErrors = new List<ValidationResult>
                    {new ValidationResult($"Book Genre with ID: {ID} Not Found", null)}
            };
        }

        public async Task<ResultModel<string>> EditABookGenre(EditBookGenresDTO bookGenresDTO)
        {
            var bookGenre = await _bookGenre.DataStore.GetAllQuery().Where(x => x.ID == bookGenresDTO.ID).FirstOrDefaultAsync();

            if (bookGenre is null)
                return new ResultModel<string>
                {
                    Data = "Failed",
                    Message = $"Book Genre with Name: {bookGenresDTO.Name} does not Exist",
                    ValidationErrors = new List<ValidationResult>
                    {new ValidationResult($"Book Genre with Name: {bookGenresDTO.Name} does not Exist", null)}
                };

            bookGenre.LastModificationTime = DateTime.Now;
            bookGenre.Name = bookGenresDTO.Name is null ? bookGenre.Name : bookGenresDTO.Name;
            bookGenre.Description = bookGenresDTO.Description is null ? bookGenre.Description : bookGenresDTO.Description;

            _bookGenre.DataStore.Update(bookGenre);
            await _bookGenre.SaveChanges();

            return new ResultModel<string> { Data = "Success", Message = "Book Genre Updated Successfully" };
        }

        public async Task<ResultModel<BookGenresVM>> ViewABooksGenre(Guid ID)
        {
            var bookGenre = await _bookGenre.DataStore.GetAllQuery()
                .Where(x => x.ID == ID)
                .Include(x => x.Books)
                .FirstOrDefaultAsync();

            if (bookGenre is null) return new ResultModel<BookGenresVM>($"Book Genre with ID: {ID} Not Found");

            return new ResultModel<BookGenresVM> { Data = bookGenre, Message = "Success" };
            
        }

        public async Task<ResultModel<PagedList<BookGenresVM>>> ViewAllBooksGenres(SearchVM model)
        {
            var query =  _bookGenre.DataStore.GetAllQuery();

            return await SearchBooksGenre(query, model);
        }

        private async Task<ResultModel<PagedList<BookGenresVM>>> SearchBooksGenre(IQueryable<BookGenres> query, SearchVM model)
        {
            if (model.Filter is not null)
                query = BuildQueryFilter(query, model.Filter);

            var bookGenres = await query.OrderByDescending(x => x.CreationTime)
                .ToPagedListAsync(model.PageIndex, model.PageSize);

            var bookGenresVms = bookGenres.Select(x => (BookGenresVM)x).ToList();

            var data = new PagedList<BookGenresVM>(bookGenresVms, model.PageIndex, model.PageSize, bookGenres.TotalItemCount);

            return new ResultModel<PagedList<BookGenresVM>> { Data = data, Message = $"Found {bookGenres.Count} Genres." };
        }

        private IQueryable<BookGenres> BuildQueryFilter(IQueryable<BookGenres> query, Filter filter)
        {
           
            switch (filter.FilterColumn)
            {
                case "Name":
                    query = query.Where(x => x.Name.ToLower().Contains(filter.Keyword.ToLower()));
                    break;
                default:
                    break;
            }
            
            return query;
        }
    }
}

using LM.Domain.Common.ViewModel;
using LM.Domain.Common;
using LM.Domain.Utils.Pagination;
using System.Threading.Tasks;
using LM.DTOs.Request.BookDTO;
using LM.DTOs.Response.BookVM;
using System;

namespace LM.Application.Interfaces.Services
{
    public interface IBookService
    {
        #region Books
        Task<ResultModel<string>> AddNewBook(BookDTO bookGDTO);

        Task<ResultModel<PagedList<BookVM>>> GetAllBooks(SearchVM model);

        Task<ResultModel<BookVM>> GetABookByID(Guid ID);

        Task<ResultModel<BookVM>> GetABookByISBN(string isbn);

        Task<ResultModel<string>> EditABook(EditBookDTO bookGDTO);

        Task<ResultModel<string>> DeleteABook(Guid ID);
        #endregion

        #region BookInventory
        Task<ResultModel<PagedList<BookInventoryVM>>> GetAllBooksInventory(pagiSearchVm model);

        Task<ResultModel<string>> UpdateInventory(BookInventoryUpdateDTO bookInvDTO);

        #endregion
    }
}

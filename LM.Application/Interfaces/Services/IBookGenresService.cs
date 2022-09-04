using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Request.BookGenreDTO;
using LM.DTOs.Response.BookGenresVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Application.Interfaces.Services
{
    public interface IBookGenresService
    {
        #region BookGenres
        Task<ResultModel<string>> AddBookGenre(BookGenresDTO bookGenresDTO);

        Task<ResultModel<PagedList<BookGenresVM>>> ViewAllBooksGenres(SearchVM model);

        Task<ResultModel<BookGenresVM>> ViewABooksGenre(Guid ID);

        Task<ResultModel<string>> EditABookGenre(EditBookGenresDTO bookGenresDTO);

        Task<ResultModel<string>> DeleteABookGenre(Guid ID);
        #endregion
    }
}

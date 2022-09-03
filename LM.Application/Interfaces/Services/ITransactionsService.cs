using LM.Domain.Common.ViewModel;
using LM.Domain.Common;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Request.BookDTO;
using LM.DTOs.Response.BookVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LM.DTOs.Response;

namespace LM.Application.Interfaces.Services
{
    public interface ITransactionsService
    {
        #region Books
        Task<ResultModel<IssueBookVm>> IssueBook(Guid bookID);

        Task<ResultModel<returnBookVm>> ReturnBook(Guid bookID);

        Task<ResultModel<PagedList<BookHistoryVM>>> UserOverAllHistory(pagiSearchVm model);

        Task<ResultModel<PagedList<BookHistoryVM>>> UserOverAllHistory(pagiSearchVm model, Guid userId);

        #endregion
    }
}

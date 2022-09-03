using LM.Application.Interfaces.Services;
using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Enums;
using LM.DTOs.Request.BookDTO;
using LM.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using LM.DTOs.Response;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Response.BookVM;
using Microsoft.AspNetCore.Authorization;

namespace LM.API.Controllers.v1
{
    /// <summary>
    /// This controller handles all Transactions processes of this application.
    /// Issue, Return, User Borrow History etc.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TransactionController : BaseController
    {
        private readonly ITransactionsService _transactionsService;

        /// <summary>
        /// Constructor for dependency injections
        /// </summary>
        /// <param name="transactionsService"></param>
        public TransactionController(ITransactionsService transactionsService)
        {
            this._transactionsService = transactionsService;
        }

        /// <summary>
        /// This endpoint Issues out Book to Customers.
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        [HttpPost("BorrowBook")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin, Customers")]
        [ProducesResponseType(typeof(ApiResponse<IssueBookVm>), 200)]
        public async Task<IActionResult> IssueBook([FromQuery] Guid bookID)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _transactionsService.IssueBook(bookID);

                if (!result.HasError)
                    return ApiResponse<IssueBookVm>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: result.Data, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint Records the returned Book from Customers.
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        [HttpPost("ReturnBook")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin, Customers")]
        [ProducesResponseType(typeof(ApiResponse<returnBookVm>), 200)]
        public async Task<IActionResult> ReturnBook([FromQuery] Guid bookID)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _transactionsService.ReturnBook(bookID);

                if (!result.HasError)
                    return ApiResponse<returnBookVm>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: result.Data, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint return Customers Book History Records.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("BookHistory")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin, Customers")]
        [ProducesResponseType(typeof(ApiResponse<PagedList<BookHistoryVM>>), 200)]
        public async Task<IActionResult> UserOverAllHistory([FromQuery] pagiSearchVm model)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _transactionsService.UserOverAllHistory(model);

                if (!result.HasError)
                    return ApiResponse<PagedList<BookHistoryVM>>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: result.Data.TotalItemCount);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint return Customers Book History Records Requested by Clients.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("ClientRequest")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdmin, Clients")]
        [ProducesResponseType(typeof(ApiResponse<PagedList<BookHistoryVM>>), 200)]
        public async Task<IActionResult> UserOverAllHistory([FromQuery] pagiSearchVm model, Guid userId)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _transactionsService.UserOverAllHistory(model, userId);

                if (!result.HasError)
                    return ApiResponse<PagedList<BookHistoryVM>>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: result.Data.TotalItemCount);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}

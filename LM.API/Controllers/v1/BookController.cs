using LM.Application.Interfaces.Services;
using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Enums;
using LM.DTOs.Request.BookGenreDTO;
using LM.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using LM.DTOs.Request.BookDTO;
using LM.Domain.Utils.Pagination;
using LM.DTOs.Response.BookGenresVM;
using LM.DTOs.Response.BookVM;
using Microsoft.AspNetCore.Authorization;
using LM.DTOs.Response;
using LM.Domain.Utils;

namespace LM.API.Controllers.v1
{
    /// <summary>
    /// This controller handles all Book processes of this application.
    /// Registration, Update and all account verification of user.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        private readonly ITransactionsService _transactionsService;

        /// <summary>
        /// Constructor for dependency injections
        /// </summary>
        /// <param name="bookService"></param>
        /// /// <param name="transactionsService"></param>
        public BookController(IBookService bookService, ITransactionsService transactionsService)
        {
            _bookService = bookService;
            _transactionsService = transactionsService;
        }

        /// <summary>
        /// This endpoint creates a new Book.
        /// </summary>
        /// <param name="bookGDTO"></param>
        /// <returns></returns>
        [HttpPost("Book")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), 200)]
        public async Task<IActionResult> AddNewBook([FromBody] BookDTO bookGDTO)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _bookService.AddNewBook(bookGDTO);

                if (!result.HasError)
                    return ApiResponse<Guid>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: result.Data, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint gets all Books.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("Books")]
        [ProducesResponseType(typeof(ApiResponse<PagedList<BookVM>>), 200)]
        public async Task<IActionResult> GetAllBooks([FromQuery] SearchVM model)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _bookService.GetAllBooks(model);

                if (!result.HasError)
                    return ApiResponse<PagedList<BookVM>>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: result.Data.TotalItemCount);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint gets a Book by ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("ID")]
        [ProducesResponseType(typeof(ApiResponse<BookVM>), 200)]
        public async Task<IActionResult> GetABookByID([FromQuery] Guid ID)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _bookService.GetABookByID(ID);

                if (!result.HasError)
                    return ApiResponse<BookVM>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.NOT_FOUND, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint gets a Book by ISBN.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        [HttpGet("ISBN")]
        [ProducesResponseType(typeof(ApiResponse<BookVM>), 200)]
        public async Task<IActionResult> GetABookByISBN([FromQuery] string isbn)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _bookService.GetABookByISBN(isbn);

                if (!result.HasError)
                    return ApiResponse<BookVM>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.NOT_FOUND, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint gets the Inventory Records.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("Inventory")]
        [ProducesResponseType(typeof(ApiResponse<PagedList<BookInventoryVM>>), 200)]
        public async Task<IActionResult> ViewInventory([FromQuery] pagiSearchVm model)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _bookService.GetAllBooksInventory(model);

                if (!result.HasError)
                    return ApiResponse<PagedList<BookInventoryVM>>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: result.Data.TotalItemCount);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint Issues out Book to Customers.
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        [HttpPost("Borrow")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin + " , " + CoreConstants.Customer)]
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
        [HttpPost("Return")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin + " , " + CoreConstants.Customer)]
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
        [HttpGet("History")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin + " , " + CoreConstants.Customer)]
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
        [HttpGet("History/UserID")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin + " , " + CoreConstants.Client)]
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

        /// <summary>
        /// This endpoint return Book History Records.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="bookID"></param>
        /// <returns></returns>
        [HttpGet("History/BookID")]
        [ProducesResponseType(typeof(ApiResponse<PagedList<BookHistVm>>), 200)]
        public async Task<IActionResult> BookOverAllHistory([FromQuery] pagiSearchVm model, Guid bookID)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _transactionsService.BookOverAllHistory(model, bookID);

                if (!result.HasError)
                    return ApiResponse<PagedList<BookHistVm>>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: result.Data.TotalItemCount);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint updates a Book Details
        /// </summary>
        /// <param name="bookDTO"></param>
        /// <returns></returns>
        [HttpPut("EditBook")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin)]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> EditABook([FromForm] EditBookDTO bookDTO)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _bookService.EditABook(bookDTO);

                if (!result.HasError)
                    return ApiResponse<string>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse<string>(message: result.Message, codes: ApiResponseCodes.ERROR, data: result.Data, errors: result.GetErrorMessages().ToArray());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint updates a Book Inventory (quantity added)
        /// </summary>
        /// <param name="bookInvDTO"></param>
        /// <returns></returns>
        [HttpPut("UpdateInventory")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin)]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> UpdateInventory([FromForm] BookInventoryUpdateDTO bookInvDTO)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await _bookService.UpdateInventory(bookInvDTO);

                if (!result.HasError)
                    return ApiResponse<string>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse<string>(message: result.Message, codes: ApiResponseCodes.ERROR, data: result.Data, errors: result.GetErrorMessages().ToArray());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint Deletes a Book.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin)]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> DeleteBook([FromQuery] Guid ID)
        {
            try
            {
                var result = await _bookService.DeleteABook(ID);

                if (!result.HasError)
                    return ApiResponse<string>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse<string>(message: result.Message, codes: ApiResponseCodes.NOT_FOUND, data: result.Data, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

    }
}

using LM.Domain.Common;
using LM.Domain.Common.ViewModel;
using LM.Domain.Enums;
using LM.Domain.Utils.Pagination;
using LM.Domain.Utils;
using LM.DTOs.Request.BookGenreDTO;
using LM.DTOs.Response.BookGenresVM;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using LM.Services.Implementations.Features.BookGenreFeatures.Queries;
using LM.Services.Implementations.Features.BookGenreFeatures.Commands;
using SearchVM = LM.Services.Implementations.Features.BookGenreFeatures.Queries.SearchVM;

namespace LM.API.Controllers.v1.Mediator
{
    /// <summary>
    /// This controller handles all Book Genres processes of this application.
    /// Registration, Update and all account verification of user.
    /// </summary>
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MediatorBookGenresController : BaseController
    {
        //private readonly IMediator Mediator;

        private IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        //public MediatorBookGenresController(IMediator mediator)
        //{
        //    Mediator = mediator;
        //}

        /// <summary>
        /// 
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// This endpoint creates Book Genre.
        /// </summary>
        /// <param name="bookGenresDTO"></param>
        /// <returns>IActionResult.</returns>
        [HttpPost("BookGenre")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin)]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> AddBookGenre([FromBody] BookGenresDTO bookGenresDTO)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await Mediator.Send(bookGenresDTO);

                if (!result.HasError)
                    return ApiResponse<string>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: result.Data, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint gets all Book Genres.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>IActionResult.</returns>
        [HttpGet("BookGenres")]
        [ProducesResponseType(typeof(ApiResponse<PagedList<BookGenresVM>>), 200)]
        public async Task<IActionResult> GetAllBooksGenres([FromQuery] SearchVM model)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await Mediator.Send(model);

                if (!result.HasError)
                    return ApiResponse<PagedList<BookGenresVM>>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: result.Data.TotalItemCount);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.ERROR, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint gets a Book Genres and all Books under it.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>IActionResult.</returns>
        [HttpGet("ID")]
        [ProducesResponseType(typeof(ApiResponse<BookGenresVM>), 200)]
        public async Task<IActionResult> GetABooksGenres([FromQuery] Guid ID)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await Mediator.Send(new GetBookGenreByIdQuery { ID = ID } );

                if (!result.HasError)
                    return ApiResponse<BookGenresVM>(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: 1);

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.NOT_FOUND, data: false, totalCount: 0, errors: result.GetErrorMessages().ToArray());
            }

            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        /// <summary>
        /// This endpoint updates a Book Genre Details
        /// </summary>
        /// <param name="bookGenresDTO"></param>
        /// <returns>IActionResult.</returns>
        [HttpPut()]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin)]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> UpdateBookGenre([FromForm] EditBookGenresDTO bookGenresDTO)
        {
            if (!ModelState.IsValid) return ApiResponse(GetListModelErrors(), codes: ApiResponseCodes.INVALID_REQUEST);

            try
            {
                var result = await Mediator.Send(bookGenresDTO);

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
        /// This endpoint Deletes a Book Genre.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>IActionResult.</returns>
        [HttpDelete()]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = CoreConstants.SuperAdmin)]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        public async Task<IActionResult> DeleteBookGenre([FromQuery] Guid ID)
        {
            try
            {
                var result = await Mediator.Send(new DeleteBookGenreCommand { ID = ID });

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

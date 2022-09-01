using LM.Domain.Common.ViewModel;
using LM.Domain.Enums;
using LM.Domain.Enums.EnumExtension;
using LM.Domain.Utils;
using log4net;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Common
{
    /// <summary>
    /// Class BaseController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        public BaseController()
        {
            _logger = LogManager.GetLogger(typeof(BaseController));
        }

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>The current user.</value>
        protected UserPrincipal CurrentUser => new UserPrincipal(User);

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        protected Guid UserId
        {
            get { return Guid.Parse(CurrentUser.Claims.FirstOrDefault(x => x.Type == CoreConstants.UserIdKey)?.Value); }
        }

        /// <summary>
        /// Read ModelError into string collection
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        protected List<string> GetListModelErrors()
        {
            return ModelState.Values
                .SelectMany(x => x.Errors
                    .Select(ie => ie.ErrorMessage))
                .ToList();
        }

        /// <summary>
        /// Handles the error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="customErrorMessage">The custom error message.</param>
        /// <returns>IActionResult.</returns>
        protected IActionResult HandleError(Exception ex, string customErrorMessage = null)
        {
            _logger.Error(ex.StackTrace, ex);

            var rsp = new ApiResponse<string>();
            rsp.Code = ApiResponseCodes.ERROR;
#if DEBUG
            rsp.Description = $"Error: {ex?.InnerException?.Message ?? ex.Message} --> {ex?.StackTrace}";
            return Ok(rsp);
#else
            rsp.Description = customErrorMessage ?? "An error occurred while processing your request!";
            return Ok(rsp);
#endif
        }

        /// <summary>
        /// APIs the response.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="message">The message.</param>
        /// <param name="codes">The codes.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="errors">The errors.</param>
        /// <returns>IActionResult.</returns>
        public IActionResult ApiResponse<T>(T data = default, string message = "",
            ApiResponseCodes codes = ApiResponseCodes.OK, int? totalCount = 0, params string[] errors)
        {
            var response = new ApiResponse<T>(data, message, codes, totalCount, errors);
            response.Description = message ?? response.Code.GetDescription();
            return Ok(response);
        }
    }
}

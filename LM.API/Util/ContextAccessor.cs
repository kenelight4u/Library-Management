using LM.Application.Interfaces.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace LM.API.Util
{
    public class ContextAccessor : IContextAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// Contructor for dependency injections.
        /// </summary>
        /// <param name="contextAccessor"></param>
        public ContextAccessor(
            IHttpContextAccessor contextAccessor
            )
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// This returns the ID of the currently signed in user.
        /// </summary>
        /// <returns></returns>
        public Guid GetCurrentUserId()
        {
            var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            var claim = identity.Claims;

            // Gets userId from claims as string.
            var userIdClaim = claim
                .First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            //convert to Guid
            return Guid.Parse(userIdClaim);
        }

        /// <summary>
        /// This gets the email of the currently signed in user.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUserEmail()
        {
            var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            // Gets list of claims.
            var claim = identity.Claims;

            // Gets user email from claims. Generally it's a  string.
            var loggedInUSerEmail = claim
                .First(x => x.Type == ClaimTypes.Email).Value;

            return loggedInUSerEmail;
        }
    }
}

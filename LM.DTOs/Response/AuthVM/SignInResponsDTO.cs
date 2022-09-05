using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LM.DTOs.Response.AuthVM
{
    /// <summary>
    /// Class SignInResponse
    /// </summary>
    public class SignInResponsDTO
    {
        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        /// <value>The Email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets Full Name.
        /// </summary>
        /// <value>The Full Name.</value>
        public string Fullname { get; set; }
        /// <summary>
        /// Gets or sets List of Role.
        /// </summary>
        /// <value>The Role.</value>
        public List<string> Role { get; set; }
        /// <summary>
        /// Gets or sets the UserID.
        /// </summary>
        /// <value>The User ID.</value>
        public Guid UserId { get; set; }
        /// <summary>
        /// Gets or sets the Token.
        /// </summary>
        /// <value>The Token.</value>
        public string Token { get; set; }
        /// <summary>
        /// Gets or sets the Expiry Time
        /// </summary>
        /// <value>The Expiry Time.</value>
        public DateTime ExpiryTime { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Request.AuthDTO
{
    /// <summary>
    /// The SignUpDTO
    /// </summary>
    public class SignUpDTO
    {
        /// <summary>
        /// Gets or sets the Emails.
        /// </summary>
        /// <value>The Emails.</value>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets First Name.
        /// </summary>
        /// <value>The First Name.</value>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets Last Name.
        /// </summary>
        /// <value>The Last Name.</value>
        [Required]
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets Phone Number.
        /// </summary>
        /// <value>The Phone Number.</value>
        [Required]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets Password.
        /// </summary>
        /// <value>The Password.</value>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets Confirm Password.
        /// </summary>
        /// <value>The Confirm Password.</value>
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    /// <summary>
    /// Class EditDTO
    /// </summary>
    public class EditDTO
    {
        /// <summary>
        /// Gets or sets First Name.
        /// </summary>
        /// <value>The First Name.</value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets Last Name.
        /// </summary>
        /// <value>The Last Name.</value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets Phone Number.
        /// </summary>
        /// <value>The Phone Number.</value>
        public string PhoneNumber { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace LM.Domain.Entities
{
    /// <summary>
    /// Class LM User
    /// Implements the <see cref="IdentityUser"/>
    /// </summary>
    public class LMUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Registered Date.
        /// </summary>
        /// <value>The Registered Date.</value>
        public DateTime RegisteredDate { get; set; }
        /// <summary>
        /// Gets or sets the Password Reset Date.
        /// </summary>
        /// <value>The Password Reset Date.</value>
        public DateTime PasswordResetDate { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        public bool IsActive { get; set; }
        /// <summary>
        /// Gets or sets the Book Histories.
        /// </summary>
        /// <value>The Book Histories.</value>
        public List<BookHistory> BookHistories { get; set; } = new List<BookHistory>();
    }
}

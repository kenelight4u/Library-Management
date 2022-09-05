using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Request.AuthDTO
{
    /// <summary>
    /// Class ChangePasswordDTO
    /// </summary>
    public class ChangePasswordDTO
    {
        /// <summary>
        /// Gets or sets Old Password.
        /// </summary>
        /// <value>The Old Password.</value>
        public string OldPassword { get; set; }
        /// <summary>
        /// Gets or sets New Password.
        /// </summary>
        /// <value>The New Password.</value>
        public string NewPassword { get; set; }
    }
}

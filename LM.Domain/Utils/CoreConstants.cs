using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Utils
{
    /// <summary>
    /// Class CoreConstants.
    /// </summary>
    public abstract class CoreConstants
    {
        /// <summary>
        /// The date format
        /// </summary>
        public const string DateFormat = "dd MMMM, yyyy";
        /// <summary>
        /// The time format
        /// </summary>
        public const string TimeFormat = "hh:mm tt";
        /// <summary>
        /// The system date format
        /// </summary>
        public const string SystemDateFormat = "dd/MM/yyyy";
        /// <summary>
        /// The permission
        /// </summary>
        public const string Permission = nameof(Permission);
        /// <summary>
        /// The role
        /// </summary>
        public const string Role = "role";

        /// <summary>
        /// The UserIdKey
        /// oid is used by Azure AD
        /// </summary>
        public const string UserIdKey = "oid";

        /// <summary>
        /// The valid excels
        /// </summary>
        public static readonly string[] validExcels = { ".xls", ".xlsx" };

        /// <summary>
        /// Class PaginationConsts.
        /// </summary>
        public static class PaginationConsts
        {
            /// <summary>
            /// The page size
            /// </summary>
            public const int PageSize = 25;
            /// <summary>
            /// The page index
            /// </summary>
            public const int PageIndex = 1;
        }

        /// <summary>
        /// Class AllowedFileExtensions.
        /// </summary>
        public static class AllowedFileExtensions
        {
            /// <summary>
            /// The signature
            /// </summary>
            public const string Signature = ".jpg,.png";
        }
       
    }
}

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
        /// The Require Customers
        /// </summary>
        public const string RequireCustomers = "RequireCustomers";
        /// <summary>
        /// The Require Clients
        /// </summary>
        public const string RequireClients = "RequireClients";
        /// <summary>
        /// The SuperAdmin
        /// </summary>
        public const string SuperAdmin = "SuperAdmin";

        /// <summary>
        /// The SuperAdmin
        /// </summary>
        public const string Customer = "Customer";

        /// <summary>
        /// The SuperAdmin
        /// </summary>
        public const string Client = "Client";
        /// <summary>
        /// The permission
        /// </summary>
        public const string Permission = nameof(Permission);
        /// <summary>
        /// The role
        /// </summary>
        public const string Role = "role";

        /// <summary>
        /// The policy name 
        /// </summary>
        public const string RequireSuperAdmin = "RequireSuperAdmin";

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Application.Interfaces.Utilities
{
    /// <summary>
    /// This interface accesses the details of the currently signed in user.
    /// </summary>
    public interface IContextAccessor
    {
        // <summary>
        /// This returns the ID of the currently signed in user.
        /// </summary>
        /// <returns></returns>
        Guid GetCurrentUserId();

        /// <summary>
        /// This returns the email address of the currently signed in user.
        /// </summary>
        /// <returns></returns>
        string GetCurrentUserEmail();
    }
}

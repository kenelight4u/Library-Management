using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Enums
{
    public enum BookCustomerStatus
    {
        [Description("Issue Out")]
        IssuedOut = 1,
        [Description("Returned")]
        Returned
    }
}

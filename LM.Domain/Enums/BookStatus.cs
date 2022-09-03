using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Enums
{
    public enum BookStatus
    {
        [Description("In Stock")]
        InStock = 1,
        [Description("Out of Stock")]
        OutOfStock
    }
}

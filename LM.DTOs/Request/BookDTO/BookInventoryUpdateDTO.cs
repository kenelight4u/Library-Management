using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Request.BookDTO
{
    /// <summary>
    /// Class of BookInventorry UpdateDTO
    /// </summary>
    public class BookInventoryUpdateDTO
    {
        /// <summary>
        /// Gets or sets the BookID.
        /// </summary>
        /// <value>The Book ID.</value>
        [Required] public Guid BookID { get; set; }

        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        [Required] public int Quantity { get; set; }
    }
}

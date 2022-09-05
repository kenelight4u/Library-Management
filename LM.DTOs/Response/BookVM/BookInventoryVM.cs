using LM.Domain.Entities;
using LM.DTOs.Response.BookGenresVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response.BookVM
{
    /// <summary>
    /// Class Book Inventory
    /// </summary>
    public class BookInventoryVM
    {
        // <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the BookShortVM
        /// </summary>
        /// <value>The BookShort vm.</value>
        public BookShortVM Books { get; set; }

        /// <summary>
        /// Converts BookInventory to BookGenre VM
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BookInventoryVM.</returns
        public static implicit operator BookInventoryVM(BookInventory model)
        {
            return model == null
                ? null
                : new BookInventoryVM
                {
                   Quantity = model.Quantity,
                   Books = model.Books
                };
        }
    }
}

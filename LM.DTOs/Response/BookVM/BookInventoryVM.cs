using LM.Domain.Entities;
using LM.DTOs.Response.BookGenresVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response.BookVM
{
    public class BookInventoryVM
    {
        public int Quantity { get; set; }

        public BookShortVM Books { get; set; }

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

using LM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response.BookGenresVM
{
    public class BookShortVM
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public static implicit operator BookShortVM(Book model)
        {
            return model == null
                ? null
                : new BookShortVM
                {
                    Author = model.Author,
                    Title = model.Title,
                    ISBN = model.ISBN
                };
        }
    }
}

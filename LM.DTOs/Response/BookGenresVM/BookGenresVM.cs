using LM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response.BookGenresVM
{
    public class BookGenresVM
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<BookShortVM> Books { get; set; } = new List<BookShortVM>();

        public static implicit operator BookGenresVM(BookGenres model)
        {
            return model == null
                ? null
                : new BookGenresVM
                {
                    ID = model.ID,
                    Name = model.Name,
                    Description = model.Description,
                    Books = model.Books.Select(x => (BookShortVM)x).ToList()
                };
        }
    }
}

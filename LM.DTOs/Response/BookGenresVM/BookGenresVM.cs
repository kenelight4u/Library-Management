using LM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response.BookGenresVM
{
    /// <summary>
    /// Class of BookGenreVM
    /// </summary>
    public class BookGenresVM
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public Guid ID { get; set; }
        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; set; }
        // <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public string Description { get; set; }
        // <summary>
        /// Gets or sets the Books.
        /// </summary>
        /// <value>The Book List.</value>
        public List<BookShortVM> Books { get; set; } = new List<BookShortVM>();

        /// <summary>
        /// Converts BookGenres to BookGenre VM
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BookGenresVM.</returns>
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

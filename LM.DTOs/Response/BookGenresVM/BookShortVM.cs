using LM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response.BookGenresVM
{
    /// <summary>
    /// Class of BookShort VM
    /// </summary>
    public class BookShortVM
    {
        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        /// <value>The ISBN.</value>
        public string ISBN { get; set; }
        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        /// <value>The Title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the Author
        /// </summary>
        /// <value>The Author.</value>
        public string Author { get; set; }

        /// <summary>
        /// Converts Book to Book VM
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BookShortVM.</returns
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

using LM.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Entities
{
    /// <summary>
    /// Class Book Inventory
    /// implements the <see cref="BaseEntity"/>
    /// </summary>
    public class BookGenres : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the Book.
        /// </summary>
        /// <value>The Books.</value>
        public List<Book> Books { get; set; } = new List<Book>();
    }
}

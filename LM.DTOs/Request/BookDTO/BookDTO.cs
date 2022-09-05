using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Request.BookDTO
{
    /// <summary>
    /// Class BookDTO
    /// </summary>
    public class BookDTO
    {
        /// <summary>
        /// Gets or sets the ISBN.
        /// </summary>
        /// <value>The ISBN.</value>
        [Required] public string ISBN { get; set; }
        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        /// <value>The Title.</value>
        [Required] public string Title { get; set; }
        /// <summary>
        /// Gets or sets the Author
        /// </summary>
        /// <value>The Author.</value>
        [Required] public string Author { get; set; }
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        [Required] public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the BookGenre ID
        /// </summary>
        /// <value>The BookGenreID.</value>
        [Required] public Guid BookGenresId { get; set; }
    }

    /// <summary>
    /// Class EditBookDTO
    /// </summary>
    public class EditBookDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [Required] public Guid ID { get; set; }
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
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }

    }

    /// <summary>
    /// Class Edit Book Quantity DTO
    /// </summary>
    public class EditBookQuantityDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        [Required] public Guid ID { get; set; }
        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public int Quantity { get; set; }
    }
}

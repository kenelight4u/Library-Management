using LM.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Entities
{
    public class Book : BaseEntity
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
        /// Gets or sets the Author.
        /// </summary>
        /// <value>The Author.</value>
        public string Author { get; set; }
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public int Quantity { get; set; }

        public Guid? BookGenresId { get; set; }

        [ForeignKey(nameof(BookGenresId))]
        public virtual BookGenres BookGenres { get; set; }

        public List<BookHistory> BookHistories { get; set; } = new List<BookHistory>();
    }
}

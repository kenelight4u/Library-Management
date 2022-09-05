using LM.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Entities
{
    /// <summary>
    /// Class Book Inventory
    /// implements the <see cref="BaseEntity"/>
    /// </summary>
    public class BookInventory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the BookID.
        /// </summary>
        /// <value>The Book ID.</value>
        public Guid? BookId { get; set; }
        /// <summary>
        /// Gets or sets the Book.
        /// </summary>
        /// <value>The Books.</value>
        [ForeignKey(nameof(BookId))]
        public virtual Book Books { get; set; }
    }
}

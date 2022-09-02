using LM.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Entities
{
    public class BookInventory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public int Quantity { get; set; }

        public Guid? BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Books { get; set; }
    }
}

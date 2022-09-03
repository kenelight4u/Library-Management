using LM.Domain.Common;
using LM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Domain.Entities
{
    public class BookHistory : BaseEntity
    {
        public Guid? BookId { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Books { get; set; }

        public string LMUserId { get; set; }

        [ForeignKey(nameof(LMUserId))]
        public virtual LMUser LMUsers { get; set; }

        /// <summary>
        /// Gets or sets the BorrowStatus.
        /// </summary>
        /// <value>The BorrowStatus.</value>
        public BookCustomerStatus BorrowStatus { get; set; }

        /// <summary>
        /// Gets or sets the ReturnedDate.
        /// </summary>
        /// <value>The ReturnedDate.</value>
        public DateTime IssuedDate { get; set; }

        /// <summary>
        /// Gets or sets the ReturnedDatee.
        /// </summary>
        /// <value>The ReturnedDate.</value>
        public DateTime ReturnedDate { get; set; }
    }
}

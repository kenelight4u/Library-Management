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
    /// <summary>
    /// Class Book Inventory
    /// implements the <see cref="BaseEntity"/>
    /// </summary>
    public class BookHistory : BaseEntity
    {
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
        /// <summary>
        /// Gets or sets the LMUserID.
        /// </summary>
        /// <value>The LMUser ID.</value>
        public string LMUserId { get; set; }
        /// <summary>
        /// Gets or sets the LM User.
        /// </summary>
        /// <value>The LM User.</value>
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

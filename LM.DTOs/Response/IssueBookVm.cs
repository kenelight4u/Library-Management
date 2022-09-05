using LM.Domain.Entities;
using LM.Domain.Enums;
using LM.DTOs.Response.BookGenresVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response
{
    /// <summary>
    /// Class of IssueBookVM
    /// </summary>
    public class IssueBookVm
    {
        /// <summary>
        /// Gets or sets the ExpectedReturnDate.
        /// </summary>
        /// <value>The ExpectedReturnDate.</value>
        public string ExpectedReturnDate { get; set; }
        /// <summary>
        /// Gets or sets the BookDetails.
        /// </summary>
        /// <value>The BookDetails.</value>
        public BookShortVM BookDetails { get; set; }
    }

    /// <summary>
    /// Class ReturnBookVM
    /// </summary>
    public class returnBookVm
    {
        /// <summary>
        /// Gets or sets the BorrowedDate.
        /// </summary>
        /// <value>The BorrowedDate.</value>
        public string BorrowedDate { get; set; }
        /// <summary>
        /// Gets or sets the ExpectedReturnDate.
        /// </summary>
        /// <value>The ExpectedReturnDate.</value>
        public string ExpectedReturnDate { get; set; }
        /// <summary>
        /// Gets or sets the ReturnDate.
        /// </summary>
        /// <value>The ReturnDate.</value>
        public string ReturnedDate { get; set; }
        /// <summary>
        /// Gets or sets the NumberOfDays.
        /// </summary>
        /// <value>The NumberOfDays.</value>
        public string NumberOfDays { get; set; }
        /// <summary>
        /// Gets or sets the Charges.
        /// </summary>
        /// <value>The Charges.</value>
        public string Charges { get; set; }
        /// <summary>
        /// Gets or sets the BookDetails.
        /// </summary>
        /// <value>The BookDetails.</value>
        public BookShortVM BookDetails { get; set; }
    }
}

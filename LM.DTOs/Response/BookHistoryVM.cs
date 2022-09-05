using LM.DTOs.Response.BookGenresVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response
{
    /// <summary>
    /// Class of BookHistoryVM
    /// </summary>
    public class BookHistoryVM
    {
        /// <summary>
        /// Gets or sets the BookDetails.
        /// </summary>
        /// <value>The BookDetails.</value>
        public BookShortVM BookDetails { get; set; }
        /// <summary>
        /// Gets or sets the BookStatus.
        /// </summary>
        /// <value>The BookStatus.</value>
        public String BookStatus { get; set; }
        /// <summary>
        /// Gets or sets the IssueDate.
        /// </summary>
        /// <value>The IssueDate.</value>
        public string IssuedDate { get; set; }
        /// <summary>
        /// Gets or sets the ReturnDate.
        /// </summary>
        /// <value>The ReturnDate.</value>
        public string ReturnedDate { get; set; }
        /// <summary>
        /// Gets or sets Full Name.
        /// </summary>
        /// <value>The Full Name.</value>
        public string UserFullName { get; set; }
    }

    /// <summary>
    /// Class of BookHistoryVM
    /// </summary>
    public class BookHistVm
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
        /// Gets or sets BookStatus.
        /// </summary>
        /// <value>The BookStatus.</value>
        public String BookStatus { get; set; }
        /// <summary>
        /// Gets or sets the IssueDate.
        /// </summary>
        /// <value>The IssueDate.</value>
        public string IssuedDate { get; set; }
        /// <summary>
        /// Gets or sets the ReturnDate.
        /// </summary>
        /// <value>The ReturnDate.</value>
        public string ReturnedDate { get; set; }
        /// <summary>
        /// Gets or sets Full Name.
        /// </summary>
        /// <value>The Full Name.</value>
        public string UserFullName { get; set; }
    }
}

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
    public class IssueBookVm
    {
        /// <summary>
        /// Gets or sets the ExpectedReturnDate.
        /// </summary>
        /// <value>The ExpectedReturnDate.</value>
        public string ExpectedReturnDate { get; set; }

        public BookShortVM BookDetails { get; set; }
    }

    public class returnBookVm
    {
        public string BorrowedDate { get; set; }

        public string ExpectedReturnDate { get; set; }

        public string ReturnedDate { get; set; }

        public string NumberOfDays { get; set; }

        public string Charges { get; set; }

        public BookShortVM BookDetails { get; set; }
    }
}

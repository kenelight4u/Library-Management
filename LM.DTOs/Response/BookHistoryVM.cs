using LM.DTOs.Response.BookGenresVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Response
{
    public class BookHistoryVM
    {
        public BookShortVM BookDetails { get; set; }

        public String BookStatus { get; set; }

        public string IssuedDate { get; set; }

        public string ReturnedDate { get; set; }

        public string UserFullName { get; set; }
    }
}

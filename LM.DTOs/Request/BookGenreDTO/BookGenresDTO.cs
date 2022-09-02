using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Request.BookGenreDTO
{
    public class BookGenresDTO
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class EditBookGenresDTO
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

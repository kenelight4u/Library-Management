using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Request.BookDTO
{
    public class BookDTO
    {
        [Required] public string ISBN { get; set; }
        
        [Required] public string Title { get; set; }
        
        [Required] public string Author { get; set; }
        
        public string Description { get; set; }
        
        [Required] public int Quantity { get; set; }

        [Required] public Guid BookGenresId { get; set; }
    }

    public class EditBookDTO
    {
        [Required] public Guid ID { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

    }

    public class EditBookQuantityDTO
    {
        [Required] public Guid ID { get; set; }

        public int Quantity { get; set; }
    }
}

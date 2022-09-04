using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Request.BookDTO
{
    public class BookInventoryUpdateDTO
    {
        [Required] public Guid BookID { get; set; }

        [Required] public int Quantity { get; set; }
    }
}

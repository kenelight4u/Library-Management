using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.DTOs.Request.AuthDTO
{
    public class SignInDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

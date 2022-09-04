using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LM.DTOs.Response.AuthVM
{
    public class SignInResponsDTO
    {
        public string Email { get; set; }
        public string Fullname { get; set; }
        public List<string> Role { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}

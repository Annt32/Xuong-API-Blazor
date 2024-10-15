using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Authentication
{
    public class LoginResponse
    {
        [Required]
        public string JwtToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}

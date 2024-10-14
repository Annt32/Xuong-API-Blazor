using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Authentication
{
    public class DangKyModel
    {
        [Required]
        public  string Username { get; set; }
        [Required]
        public  string Password { get; set; }
        [Required,EmailAddress]
        public  string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}

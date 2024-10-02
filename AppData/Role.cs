using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdata
{
    public class Role
    {
        [Key]
        public Guid IdRole { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public User Users { get; set; }
    }

}

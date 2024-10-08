using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class FieldShift
    {
        [Key]
        public Guid IdFieldShift { get; set; }

        [ForeignKey("Shift")]
        public Guid IdShift { get; set; }

        //[ForeignKey("FieldDetail")]
        //public Guid IdFieldDetail { get; set; }

        public DateTime Time { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public Shift Shift { get; set; }
        //public ICollection<FieldDetail> FieldDetails { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdata
{
    public class Attendance
    {
        [Key]
        public Guid IdAttendance { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [ForeignKey("Shift")]
        public Guid IdShift { get; set; }

        public string Notes { get; set; }
        public int Status { get; set; }
        public decimal AdditionalFee { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal BankTransferAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Shift Shift { get; set; }
    }

}

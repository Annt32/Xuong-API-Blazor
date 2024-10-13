using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Invoice
    {
        [Key]
        public Guid IdInvoice { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public int Status { get; set; }
        public decimal AdditionalFee { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public User User { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }

}

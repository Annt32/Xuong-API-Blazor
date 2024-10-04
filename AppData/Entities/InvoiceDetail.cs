using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class InvoiceDetail
    {
        [Key]
        public Guid IdInvoiceDetail { get; set; }

        [ForeignKey("Invoice")]
        public Guid IdInvoice { get; set; }

        [ForeignKey("FieldShift")]
        public Guid IdFieldShift { get; set; }

        public decimal Deposit { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public Invoice Invoice { get; set; }
        public FieldShift FieldShift { get; set; }
        public ServiceField ServiceField { get; set; }
    }

}

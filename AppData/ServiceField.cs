using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdata
{
    public class ServiceField //Bảng Dịch Vụ Sân Bóng
    {
        [Key]
        public Guid IdServiceField { get; set; }
        [ForeignKey("InvoiceDetail")]
        public Guid IdInvoiceDetail { get; set; }
        public string ServiceName { get; set; }
        public int Totalquantity { get; set; }
        public decimal Totalprice { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public ICollection<RentalEquipmentDetail> RentalEquipmentDetails { get; set; }
        public ICollection<DrinkDetail> DrinkDetails { get; set; }
        public InvoiceDetail InvoiceDetail { get; set; }

    }

}

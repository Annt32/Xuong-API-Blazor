using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdata
{
    public class RentalEquipment
    {
        [Key]
        public Guid IdRentalEquipment { get; set; }

        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string RentalEquipmentName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public ICollection<RentalEquipmentDetail> RentalEquipmentDetails { get; set; }
    }

}

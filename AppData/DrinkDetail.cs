using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appdata
{
    public class DrinkDetail
    {
        [Key]
        public Guid IdDrinkDetail { get; set; }

        [ForeignKey("Drink")]
        public Guid IdDrink { get; set; }

        [ForeignKey("ServiceField")]
        public Guid IdServiceField { get; set; }
        public int Totalquantity { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public Drink Drink { get; set; }
        public ServiceField ServiceField { get; set; }
    }

}

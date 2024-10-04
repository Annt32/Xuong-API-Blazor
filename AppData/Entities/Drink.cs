using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Drink
    {
        [Key]
        public Guid IdDrink { get; set; }

        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string DrinkName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public ICollection<DrinkDetail> DrinkDetails { get; set; }
    }

}

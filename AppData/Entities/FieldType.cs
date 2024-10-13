using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class FieldType
    {
        public Guid Id { get; set; }

        [StringLength(50, ErrorMessage = "Giới hạn 50 ký tự")]
        public string Name { get; set; }

        [Range(100000, 2000000, ErrorMessage = "Giá từ 100k - 2tr")]
        public decimal Price { get; set; }
        public string Description { get; set; }

        //
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public ICollection<Field> Fields { get; set; }
    }
}

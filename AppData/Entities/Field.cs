using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace AppData.Entities
{
    public class Field // sân
    {
        [Key]
        public Guid IdField { get; set; }

        [Required(ErrorMessage = "Tên sân bóng là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên sân bóng không được vượt quá 100 ký tự")]
        public string FieldName { get; set; }

        [Required(ErrorMessage = "Loại sân bóng là bắt buộc")]
        [StringLength(100, ErrorMessage = "Loại sân bóng không được vượt quá 100 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá phải là một số lớn hơn 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        [Range(0, 1, ErrorMessage = "Trạng thái chỉ có thể là 0 hoặc 1")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        [JsonIgnore]
        public ICollection<FieldDetail> FieldDetails { get; set; } = new List<FieldDetail>();
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Appdata
{
    public class FieldDetail //Sân Chi Tiết
    {
        [Key]
        public Guid IdFieldDetail { get; set; }

        [ForeignKey("FieldShift")]
        public Guid IdFieldShift { get; set; }

        [ForeignKey("Field")]
        public Guid IdField { get; set; }

        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        [JsonIgnore]
        public Field Field { get; set; }
        [JsonIgnore]
        public FieldShift FieldShift { get; set; }
    }

}

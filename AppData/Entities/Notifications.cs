using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppData.Entities
{
    public class Notification
    {
        [Key]
        public Guid IdNotification { get; set; }

        [ForeignKey("FieldShift")]
        public Guid IdFieldShift { get; set; }

        [Required]
        [StringLength(255)]
        public string Message { get; set; }

        public bool IsViewed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public FieldShift FieldShift { get; set; }
    }
}

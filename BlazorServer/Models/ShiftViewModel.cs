using AppData.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Models
{
    public class ShiftViewModel
    {
        public Guid IdShift { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; } = "00:00:00";
        public string EndTime { get; set; } = "00:00:00";
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

    }
}

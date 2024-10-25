using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class Shift
    {
        [Key]
        public Guid IdShift { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; } = "00:00:00";
        public string EndTime { get; set; } = "00:00:00";
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        [JsonIgnore]
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        [JsonIgnore]
        public ICollection<FieldShift> FieldShifts { get; set; } = new List<FieldShift>();
    }

}

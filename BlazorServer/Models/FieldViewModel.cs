using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Models
{
    public class FieldViewModel
    {
        public Guid IdField { get; set; }
        public Guid FieldTypeId { get; set; }
        public string FieldName { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public string FieldTypeName { get; set; }
        public decimal FieldTypePrice { get; set; }
    }
}

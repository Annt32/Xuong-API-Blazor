using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.DTO.FieldType_DTO
{
    public class FieldTypeCreateRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}

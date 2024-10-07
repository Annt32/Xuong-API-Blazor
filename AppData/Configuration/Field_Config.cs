using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Configuration
{
    public class Field_Config : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasOne(x => x.FieldType).WithMany(x => x.Fields).HasForeignKey(x => x.FieldTypeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

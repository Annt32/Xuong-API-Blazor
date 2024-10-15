using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AppData.DTO;
using Microsoft.AspNetCore.Identity;
using XuongTT_API.Model;

namespace AppData.AppDbContext
{
    public class AppDBContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDBContext()
        {

        }
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<ServiceField> ServiceFields { get; set; }
        public DbSet<RentalEquipment> RentalEquipments { get; set; }
        public DbSet<RentalEquipmentDetail> RentalEquipmentDetails { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkDetail> DrinkDetails { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<ParameterType> ParameterTypes { get; set; }
        public DbSet<FieldShift> FieldShifts { get; set; }
        public DbSet<FieldType> FieldTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DO_HUY_HOANG;Initial Catalog=XuongTH2;Integrated Security=True;Trust Server Certificate=True", b => b.MigrationsAssembly("AppAPI"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}

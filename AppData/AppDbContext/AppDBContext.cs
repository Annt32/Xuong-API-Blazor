﻿using AppData.Entities;
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
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PMB8531\\SQLEXPRESS;Initial Catalog=XuongTH6;Integrated Security=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Cấu hình quan hệ nhiều-nhiều qua bảng trung gian FieldShift
			modelBuilder.Entity<FieldShift>()
				.HasKey(fs => fs.IdFieldShift);

			modelBuilder.Entity<FieldShift>()
				.HasOne(fs => fs.Field)
				.WithMany(f => f.FieldShifts)
				.HasForeignKey(fs => fs.IdField);

			modelBuilder.Entity<FieldShift>()
				.HasOne(fs => fs.Shift)
				.WithMany(s => s.FieldShifts)
				.HasForeignKey(fs => fs.IdShift);

            modelBuilder.Entity<Notification>()
            .HasKey(n => n.IdNotification);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.FieldShift)
                .WithMany()
                .HasForeignKey(n => n.IdFieldShift)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}

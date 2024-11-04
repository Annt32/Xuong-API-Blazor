﻿// <auto-generated />
using System;
using AppData.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppData.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20241015130614_15102024")]
    partial class _15102024
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppData.Entities.Attendance", b =>
                {
                    b.Property<Guid>("IdAttendance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AdditionalFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BankTransferAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CashAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdShift")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("InitialAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdAttendance");

                    b.HasIndex("IdShift");

                    b.HasIndex("UserId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("AppData.Entities.Drink", b =>
                {
                    b.Property<Guid>("IdDrink")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DrinkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDrink");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("AppData.Entities.DrinkDetail", b =>
                {
                    b.Property<Guid>("IdDrinkDetail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdDrink")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdServiceField")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Totalquantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDrinkDetail");

                    b.HasIndex("IdDrink");

                    b.HasIndex("IdServiceField");

                    b.ToTable("DrinkDetails");
                });

            modelBuilder.Entity("AppData.Entities.Field", b =>
                {
                    b.Property<Guid>("IdField")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("FieldTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdField");

                    b.HasIndex("FieldTypeId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("AppData.Entities.FieldDetail", b =>
                {
                    b.Property<Guid>("IdFieldDetail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdField")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdFieldShift")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFieldDetail");

                    b.HasIndex("IdField");

                    b.HasIndex("IdFieldShift");

                    b.ToTable("FieldDetails");
                });

            modelBuilder.Entity("AppData.Entities.FieldShift", b =>
                {
                    b.Property<Guid>("IdFieldShift")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdShift")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFieldShift");

                    b.HasIndex("IdShift");

                    b.ToTable("FieldShifts");
                });

            modelBuilder.Entity("AppData.Entities.FieldType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("FieldTypes");
                });

            modelBuilder.Entity("AppData.Entities.Invoice", b =>
                {
                    b.Property<Guid>("IdInvoice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AdditionalFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdInvoice");

                    b.HasIndex("UserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("AppData.Entities.InvoiceDetail", b =>
                {
                    b.Property<Guid>("IdInvoiceDetail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Deposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("IdFieldShift")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdInvoice")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdInvoiceDetail");

                    b.HasIndex("IdFieldShift");

                    b.HasIndex("IdInvoice");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("AppData.Entities.Parameter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParameterTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParameterTypeId");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("AppData.Entities.ParameterType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ParameterTypes");
                });

            modelBuilder.Entity("AppData.Entities.RentalEquipment", b =>
                {
                    b.Property<Guid>("IdRentalEquipment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("RentalEquipmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRentalEquipment");

                    b.ToTable("RentalEquipments");
                });

            modelBuilder.Entity("AppData.Entities.RentalEquipmentDetail", b =>
                {
                    b.Property<Guid>("IdRentalEquipmentDetail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdRentalEquipment")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdServiceField")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Totalquantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRentalEquipmentDetail");

                    b.HasIndex("IdRentalEquipment");

                    b.HasIndex("IdServiceField");

                    b.ToTable("RentalEquipmentDetails");
                });

            modelBuilder.Entity("AppData.Entities.Role", b =>
                {
                    b.Property<Guid>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdRole");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AppData.Entities.ServiceField", b =>
                {
                    b.Property<Guid>("IdServiceField")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdInvoiceDetail")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Totalprice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Totalquantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdServiceField");

                    b.HasIndex("IdInvoiceDetail")
                        .IsUnique();

                    b.ToTable("ServiceFields");
                });

            modelBuilder.Entity("AppData.Entities.Shift", b =>
                {
                    b.Property<Guid>("IdShift")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("ShiftName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdShift");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("AppData.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AppData.Entities.Attendance", b =>
                {
                    b.HasOne("AppData.Entities.Shift", "Shift")
                        .WithMany("Attendances")
                        .HasForeignKey("IdShift")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Entities.User", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shift");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AppData.Entities.DrinkDetail", b =>
                {
                    b.HasOne("AppData.Entities.Drink", "Drink")
                        .WithMany("DrinkDetails")
                        .HasForeignKey("IdDrink")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Entities.ServiceField", "ServiceField")
                        .WithMany("DrinkDetails")
                        .HasForeignKey("IdServiceField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drink");

                    b.Navigation("ServiceField");
                });

            modelBuilder.Entity("AppData.Entities.Field", b =>
                {
                    b.HasOne("AppData.Entities.FieldType", null)
                        .WithMany("Fields")
                        .HasForeignKey("FieldTypeId");
                });

            modelBuilder.Entity("AppData.Entities.FieldDetail", b =>
                {
                    b.HasOne("AppData.Entities.Field", "Field")
                        .WithMany("FieldDetails")
                        .HasForeignKey("IdField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Entities.FieldShift", "FieldShift")
                        .WithMany()
                        .HasForeignKey("IdFieldShift")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");

                    b.Navigation("FieldShift");
                });

            modelBuilder.Entity("AppData.Entities.FieldShift", b =>
                {
                    b.HasOne("AppData.Entities.Shift", "Shift")
                        .WithMany()
                        .HasForeignKey("IdShift")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shift");
                });

            modelBuilder.Entity("AppData.Entities.Invoice", b =>
                {
                    b.HasOne("AppData.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AppData.Entities.InvoiceDetail", b =>
                {
                    b.HasOne("AppData.Entities.FieldShift", "FieldShift")
                        .WithMany()
                        .HasForeignKey("IdFieldShift")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("IdInvoice")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldShift");

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("AppData.Entities.Parameter", b =>
                {
                    b.HasOne("AppData.Entities.ParameterType", "ParameterType")
                        .WithMany("Parameters")
                        .HasForeignKey("ParameterTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParameterType");
                });

            modelBuilder.Entity("AppData.Entities.RentalEquipmentDetail", b =>
                {
                    b.HasOne("AppData.Entities.RentalEquipment", "RentalEquipment")
                        .WithMany("RentalEquipmentDetails")
                        .HasForeignKey("IdRentalEquipment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Entities.ServiceField", "ServiceField")
                        .WithMany("RentalEquipmentDetails")
                        .HasForeignKey("IdServiceField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RentalEquipment");

                    b.Navigation("ServiceField");
                });

            modelBuilder.Entity("AppData.Entities.Role", b =>
                {
                    b.HasOne("AppData.Entities.User", "Users")
                        .WithOne("Role")
                        .HasForeignKey("AppData.Entities.Role", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("AppData.Entities.ServiceField", b =>
                {
                    b.HasOne("AppData.Entities.InvoiceDetail", "InvoiceDetail")
                        .WithOne("ServiceField")
                        .HasForeignKey("AppData.Entities.ServiceField", "IdInvoiceDetail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvoiceDetail");
                });

            modelBuilder.Entity("AppData.Entities.Drink", b =>
                {
                    b.Navigation("DrinkDetails");
                });

            modelBuilder.Entity("AppData.Entities.Field", b =>
                {
                    b.Navigation("FieldDetails");
                });

            modelBuilder.Entity("AppData.Entities.FieldType", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("AppData.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceDetails");
                });

            modelBuilder.Entity("AppData.Entities.InvoiceDetail", b =>
                {
                    b.Navigation("ServiceField")
                        .IsRequired();
                });

            modelBuilder.Entity("AppData.Entities.ParameterType", b =>
                {
                    b.Navigation("Parameters");
                });

            modelBuilder.Entity("AppData.Entities.RentalEquipment", b =>
                {
                    b.Navigation("RentalEquipmentDetails");
                });

            modelBuilder.Entity("AppData.Entities.ServiceField", b =>
                {
                    b.Navigation("DrinkDetails");

                    b.Navigation("RentalEquipmentDetails");
                });

            modelBuilder.Entity("AppData.Entities.Shift", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("AppData.Entities.User", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("Role")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

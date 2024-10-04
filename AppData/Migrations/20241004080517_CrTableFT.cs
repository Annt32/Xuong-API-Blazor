using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class CrTableFT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FieldTypeId",
                table: "Fields",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FieldTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_FieldTypeId",
                table: "Fields",
                column: "FieldTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_FieldTypes_FieldTypeId",
                table: "Fields",
                column: "FieldTypeId",
                principalTable: "FieldTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_FieldTypes_FieldTypeId",
                table: "Fields");

            migrationBuilder.DropTable(
                name: "FieldTypes");

            migrationBuilder.DropIndex(
                name: "IX_Fields_FieldTypeId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "FieldTypeId",
                table: "Fields");
        }
    }
}

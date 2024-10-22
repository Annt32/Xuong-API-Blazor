using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class UpdateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldShifts_Fields_FieldIdField",
                table: "FieldShifts");

            migrationBuilder.DropIndex(
                name: "IX_FieldShifts_FieldIdField",
                table: "FieldShifts");

            migrationBuilder.DropColumn(
                name: "FieldIdField",
                table: "FieldShifts");

            migrationBuilder.CreateIndex(
                name: "IX_FieldShifts_IdField",
                table: "FieldShifts",
                column: "IdField");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldShifts_Fields_IdField",
                table: "FieldShifts",
                column: "IdField",
                principalTable: "Fields",
                principalColumn: "IdField",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldShifts_Fields_IdField",
                table: "FieldShifts");

            migrationBuilder.DropIndex(
                name: "IX_FieldShifts_IdField",
                table: "FieldShifts");

            migrationBuilder.AddColumn<Guid>(
                name: "FieldIdField",
                table: "FieldShifts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FieldShifts_FieldIdField",
                table: "FieldShifts",
                column: "FieldIdField");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldShifts_Fields_FieldIdField",
                table: "FieldShifts",
                column: "FieldIdField",
                principalTable: "Fields",
                principalColumn: "IdField",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class UpdateNgayKetThuc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "369bdd73-75db-4e19-9a3d-b45373ebcaef");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKetThuc",
                table: "LopHocs",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "823d908d-b8ae-48c9-8b86-21051161a68f", "df9ff11f-1092-4b30-90bc-f6a6f5dea59c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "823d908d-b8ae-48c9-8b86-21051161a68f");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayKetThuc",
                table: "LopHocs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "369bdd73-75db-4e19-9a3d-b45373ebcaef", "454c8836-1099-4093-8f6f-dbffd9699ba3", "Admin", "ADMIN" });
        }
    }
}

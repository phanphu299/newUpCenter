using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddNgayDongHP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ea35fa2-5292-44a6-9265-361ee461db3a");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayDong",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f6a9c73-cad5-427b-91da-fec7832b954b", "5a63098d-7ab9-4f78-bff5-4d89d87749df", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f6a9c73-cad5-427b-91da-fec7832b954b");

            migrationBuilder.DropColumn(
                name: "NgayDong",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1ea35fa2-5292-44a6-9265-361ee461db3a", "0f2a7ebc-0bdd-40a1-8232-884b58b0ee91", "Admin", "ADMIN" });
        }
    }
}

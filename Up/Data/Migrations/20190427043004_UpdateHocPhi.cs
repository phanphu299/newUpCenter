using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateHocPhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60fc36d9-d5e2-465e-af20-fbf1286ee9b7");

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "HocPhis",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayApDung",
                table: "HocPhis",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70dc13f3-6011-4752-baf0-3e230bc27bbf", "ee0b19a6-1240-4d7a-b9a8-44a597d4015d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70dc13f3-6011-4752-baf0-3e230bc27bbf");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "HocPhis");

            migrationBuilder.DropColumn(
                name: "NgayApDung",
                table: "HocPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60fc36d9-d5e2-465e-af20-fbf1286ee9b7", "a136a22d-4462-4d5f-a111-1aa7006dd999", "Admin", "ADMIN" });
        }
    }
}

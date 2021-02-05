using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class RemoveNhanVienKhac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhanVienKhacs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe37f8ed-9ce2-4297-8010-cf8c6dcca898");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f6ebbdd5-fd88-4bbe-9d7d-8f6093b94b3d", "edfaac11-bd45-4919-9b01-22f81f4d5e5d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6ebbdd5-fd88-4bbe-9d7d-8f6093b94b3d");

            migrationBuilder.CreateTable(
                name: "NhanVienKhacs",
                columns: table => new
                {
                    NhanVienKhacId = table.Column<Guid>(nullable: false),
                    BasicSalary = table.Column<double>(nullable: false),
                    CMND = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DiaChi = table.Column<string>(nullable: true),
                    FacebookAccount = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVienKhacs", x => x.NhanVienKhacId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe37f8ed-9ce2-4297-8010-cf8c6dcca898", "d02b6f31-ae96-4eb6-a245-5adb032af371", "Admin", "ADMIN" });
        }
    }
}

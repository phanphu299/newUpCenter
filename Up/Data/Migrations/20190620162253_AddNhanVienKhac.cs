using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddNhanVienKhac : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f6a9c73-cad5-427b-91da-fec7832b954b");

            migrationBuilder.CreateTable(
                name: "NhanVienKhacs",
                columns: table => new
                {
                    NhanVienKhacId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    BasicSalary = table.Column<double>(nullable: false),
                    FacebookAccount = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    CMND = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVienKhacs", x => x.NhanVienKhacId);
                });

            migrationBuilder.CreateTable(
                name: "ThongKe_ChiPhis",
                columns: table => new
                {
                    ThongKe_ChiPhiId = table.Column<Guid>(nullable: false),
                    ChiPhi = table.Column<double>(nullable: false),
                    NgayChiPhi = table.Column<DateTime>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKe_ChiPhis", x => x.ThongKe_ChiPhiId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63a054e2-1393-4457-ad77-824a57dea515", "25a5f382-a31f-4d54-9067-3875fd15ff93", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhanVienKhacs");

            migrationBuilder.DropTable(
                name: "ThongKe_ChiPhis");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63a054e2-1393-4457-ad77-824a57dea515");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9f6a9c73-cad5-427b-91da-fec7832b954b", "5a63098d-7ab9-4f78-bff5-4d89d87749df", "Admin", "ADMIN" });
        }
    }
}

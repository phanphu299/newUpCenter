using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddThongKeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e39025d4-59ff-45d0-827a-22f1b8955d2c");

            migrationBuilder.CreateTable(
                name: "ThongKeGiaoVienTheoThangs",
                columns: table => new
                {
                    ThongKeGiaoVienTheoThangId = table.Column<Guid>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    LoaiGiaoVien = table.Column<byte>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKeGiaoVienTheoThangs", x => x.ThongKeGiaoVienTheoThangId);
                });

            migrationBuilder.CreateTable(
                name: "ThongKeHocVienTheoThangs",
                columns: table => new
                {
                    ThongKeHocVienTheoThangId = table.Column<Guid>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    LoaiHocVien = table.Column<byte>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKeHocVienTheoThangs", x => x.ThongKeHocVienTheoThangId);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongKeGiaoVienTheoThangs");

            migrationBuilder.DropTable(
                name: "ThongKeHocVienTheoThangs");
        }
    }
}

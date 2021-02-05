using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class ThemTableDoanhThuSach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6eb504c9-49a7-408c-9c6d-e8dc972210a7");

            migrationBuilder.CreateTable(
                name: "ThongKe_DoanhThuHocPhi_TaiLieus",
                columns: table => new
                {
                    ThongKe_DoanhThuHocPhi_TaiLieuId = table.Column<Guid>(nullable: false),
                    ThongKe_DoanhThuHocPhiId = table.Column<Guid>(nullable: false),
                    SachId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKe_DoanhThuHocPhi_TaiLieus", x => x.ThongKe_DoanhThuHocPhi_TaiLieuId);
                    table.ForeignKey(
                        name: "FK_ThongKe_DoanhThuHocPhi_TaiLieus_Sachs_SachId",
                        column: x => x.SachId,
                        principalTable: "Sachs",
                        principalColumn: "SachId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThongKe_DoanhThuHocPhi_TaiLieus_ThongKe_DoanhThuHocPhis_ThongKe_DoanhThuHocPhiId",
                        column: x => x.ThongKe_DoanhThuHocPhiId,
                        principalTable: "ThongKe_DoanhThuHocPhis",
                        principalColumn: "ThongKe_DoanhThuHocPhiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2aac3505-2556-4263-9c07-0f540307d122", "0d9bba62-e9d0-41cc-9545-a52a49d9a3fd", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_DoanhThuHocPhi_TaiLieus_SachId",
                table: "ThongKe_DoanhThuHocPhi_TaiLieus",
                column: "SachId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_DoanhThuHocPhi_TaiLieus_ThongKe_DoanhThuHocPhiId",
                table: "ThongKe_DoanhThuHocPhi_TaiLieus",
                column: "ThongKe_DoanhThuHocPhiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongKe_DoanhThuHocPhi_TaiLieus");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2aac3505-2556-4263-9c07-0f540307d122");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6eb504c9-49a7-408c-9c6d-e8dc972210a7", "22af6bc7-3119-4f6a-bd30-70cda769bf8d", "Admin", "ADMIN" });
        }
    }
}

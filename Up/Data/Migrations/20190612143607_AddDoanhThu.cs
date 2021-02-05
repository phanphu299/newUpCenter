using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddDoanhThu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30f5dc72-f0f1-47c5-86aa-4eaac7fad20a");

            migrationBuilder.CreateTable(
                name: "ThongKe_DoanhThuHocPhis",
                columns: table => new
                {
                    ThongKe_DoanhThuHocPhiId = table.Column<Guid>(nullable: false),
                    HocVienId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    HocPhi = table.Column<double>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongKe_DoanhThuHocPhis", x => x.ThongKe_DoanhThuHocPhiId);
                    table.ForeignKey(
                        name: "FK_ThongKe_DoanhThuHocPhis_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThongKe_DoanhThuHocPhis_LopHocs_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHocs",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1ea35fa2-5292-44a6-9265-361ee461db3a", "0f2a7ebc-0bdd-40a1-8232-884b58b0ee91", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_DoanhThuHocPhis_HocVienId",
                table: "ThongKe_DoanhThuHocPhis",
                column: "HocVienId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_DoanhThuHocPhis_LopHocId",
                table: "ThongKe_DoanhThuHocPhis",
                column: "LopHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ea35fa2-5292-44a6-9265-361ee461db3a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30f5dc72-f0f1-47c5-86aa-4eaac7fad20a", "b7017095-689e-42e3-92c7-cab9464789e4", "Admin", "ADMIN" });
        }
    }
}

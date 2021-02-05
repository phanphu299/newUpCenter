using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class LopHoc_DiemDanh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94496989-732c-4a8d-a47a-646a4ec9fea5");

            migrationBuilder.CreateTable(
                name: "HocVien_LopHocs",
                columns: table => new
                {
                    HocVien_LopHocId = table.Column<Guid>(nullable: false),
                    HocVienId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocVien_LopHocs", x => x.HocVien_LopHocId);
                    table.ForeignKey(
                        name: "FK_HocVien_LopHocs_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocVien_LopHocs_LopHocs_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHocs",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LopHoc_DiemDanhs",
                columns: table => new
                {
                    LopHoc_DiemDanhId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    HocVienId = table.Column<Guid>(nullable: false),
                    IsOff = table.Column<bool>(nullable: false),
                    NgayDiemDanh = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc_DiemDanhs", x => x.LopHoc_DiemDanhId);
                    table.ForeignKey(
                        name: "FK_LopHoc_DiemDanhs_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHoc_DiemDanhs_LopHocs_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHocs",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "60fc36d9-d5e2-465e-af20-fbf1286ee9b7", "a136a22d-4462-4d5f-a111-1aa7006dd999", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HocVien_LopHocs_HocVienId",
                table: "HocVien_LopHocs",
                column: "HocVienId");

            migrationBuilder.CreateIndex(
                name: "IX_HocVien_LopHocs_LopHocId",
                table: "HocVien_LopHocs",
                column: "LopHocId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_DiemDanhs_HocVienId",
                table: "LopHoc_DiemDanhs",
                column: "HocVienId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_DiemDanhs_LopHocId",
                table: "LopHoc_DiemDanhs",
                column: "LopHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocVien_LopHocs");

            migrationBuilder.DropTable(
                name: "LopHoc_DiemDanhs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60fc36d9-d5e2-465e-af20-fbf1286ee9b7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "94496989-732c-4a8d-a47a-646a4ec9fea5", "c7ac9f11-0c42-41fa-a039-6b9693eaf347", "Admin", "ADMIN" });
        }
    }
}

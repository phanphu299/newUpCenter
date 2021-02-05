using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class Addhocvien_ngayhoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a0198ab-4497-4834-920a-4119a1fc7ce9");

            migrationBuilder.CreateTable(
                name: "HocVien_NgayHocs",
                columns: table => new
                {
                    HocVien_NgayHocId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    HocVienId = table.Column<Guid>(nullable: false),
                    NgayBatDau = table.Column<DateTime>(nullable: false),
                    NgayKetThuc = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocVien_NgayHocs", x => x.HocVien_NgayHocId);
                    table.ForeignKey(
                        name: "FK_HocVien_NgayHocs_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocVien_NgayHocs_LopHocs_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHocs",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e4b69bf-3e99-418c-80bd-34e06dd30cda", "5ece560a-75de-4319-b136-1d440cffe1fa", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HocVien_NgayHocs_HocVienId",
                table: "HocVien_NgayHocs",
                column: "HocVienId");

            migrationBuilder.CreateIndex(
                name: "IX_HocVien_NgayHocs_LopHocId",
                table: "HocVien_NgayHocs",
                column: "LopHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocVien_NgayHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e4b69bf-3e99-418c-80bd-34e06dd30cda");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a0198ab-4497-4834-920a-4119a1fc7ce9", "cb352f0a-c828-4306-84f6-fc351318fb10", "Admin", "ADMIN" });
        }
    }
}

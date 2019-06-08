using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class HocVienNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81fd8a33-2c04-40c4-8257-90169f8476c4");

            migrationBuilder.CreateTable(
                name: "HocVien_Nos",
                columns: table => new
                {
                    HocVien_NoId = table.Column<Guid>(nullable: false),
                    HocVienId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    TienNo = table.Column<double>(nullable: false),
                    NgayNo = table.Column<DateTime>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocVien_Nos", x => x.HocVien_NoId);
                    table.ForeignKey(
                        name: "FK_HocVien_Nos_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocVien_Nos_LopHocs_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHocs",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "842a8d1b-d2a9-4776-8513-c9172a20d2c1", "bd32459e-2981-4cc7-979f-84db24073906", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HocVien_Nos_HocVienId",
                table: "HocVien_Nos",
                column: "HocVienId");

            migrationBuilder.CreateIndex(
                name: "IX_HocVien_Nos_LopHocId",
                table: "HocVien_Nos",
                column: "LopHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocVien_Nos");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "842a8d1b-d2a9-4776-8513-c9172a20d2c1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81fd8a33-2c04-40c4-8257-90169f8476c4", "895bbf62-13e4-44aa-b096-91fc978a9109", "Admin", "ADMIN" });
        }
    }
}

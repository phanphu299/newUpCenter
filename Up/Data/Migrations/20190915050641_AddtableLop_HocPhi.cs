using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddtableLop_HocPhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs");

            migrationBuilder.DropIndex(
                name: "IX_LopHocs_HocPhiId",
                table: "LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77d51877-e22f-4044-a320-0e0da0b582a8");

            migrationBuilder.DropColumn(
                name: "HocPhiId",
                table: "LopHocs");

            migrationBuilder.CreateTable(
                name: "LopHoc_HocPhis",
                columns: table => new
                {
                    LopHoc_HocPhiId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    HocPhiId = table.Column<Guid>(nullable: false),
                    Thang = table.Column<int>(nullable: false),
                    Nam = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc_HocPhis", x => x.LopHoc_HocPhiId);
                    table.ForeignKey(
                        name: "FK_LopHoc_HocPhis_HocPhis_HocPhiId",
                        column: x => x.HocPhiId,
                        principalTable: "HocPhis",
                        principalColumn: "HocPhiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHoc_HocPhis_LopHocs_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHocs",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d19477af-1dc0-4daf-b8f6-8fec745b49db", "207cbcfc-84d3-4b31-b5cb-b880f27aa45f", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_HocPhis_HocPhiId",
                table: "LopHoc_HocPhis",
                column: "HocPhiId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_HocPhis_LopHocId",
                table: "LopHoc_HocPhis",
                column: "LopHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LopHoc_HocPhis");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d19477af-1dc0-4daf-b8f6-8fec745b49db");

            migrationBuilder.AddColumn<Guid>(
                name: "HocPhiId",
                table: "LopHocs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "77d51877-e22f-4044-a320-0e0da0b582a8", "3f4318f8-158b-4144-9c04-a4ba83483141", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocs_HocPhiId",
                table: "LopHocs",
                column: "HocPhiId");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs",
                column: "HocPhiId",
                principalTable: "HocPhis",
                principalColumn: "HocPhiId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

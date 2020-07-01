using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateHocPhiTronGoiNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e553a1af-499b-43b1-82b2-0760c051e02d");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "HocPhiTronGois",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "HocPhiTronGoi_LopHoc",
                columns: table => new
                {
                    HocPhiTronGoi_LopHocId = table.Column<Guid>(nullable: false),
                    HocPhiTronGoiId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhiTronGoi_LopHoc", x => x.HocPhiTronGoi_LopHocId);
                    table.ForeignKey(
                        name: "FK_HocPhiTronGoi_LopHoc_HocPhiTronGois_HocPhiTronGoiId",
                        column: x => x.HocPhiTronGoiId,
                        principalTable: "HocPhiTronGois",
                        principalColumn: "HocPhiTronGoiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocPhiTronGoi_LopHoc_LopHocs_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHocs",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0288b36f-a846-4849-9ba9-7ed4dd421876", "b2fc89ee-cbd5-4de4-b942-9006c2ff240a", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HocPhiTronGoi_LopHoc_HocPhiTronGoiId",
                table: "HocPhiTronGoi_LopHoc",
                column: "HocPhiTronGoiId");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhiTronGoi_LopHoc_LopHocId",
                table: "HocPhiTronGoi_LopHoc",
                column: "LopHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocPhiTronGoi_LopHoc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0288b36f-a846-4849-9ba9-7ed4dd421876");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "HocPhiTronGois");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e553a1af-499b-43b1-82b2-0760c051e02d", "0a53ce33-2b27-4737-a611-faaae48fa253", "Admin", "ADMIN" });
        }
    }
}

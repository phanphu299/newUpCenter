using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddHocPhiTronGoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20b49ec8-92a7-445e-95c0-8ef120a44524");

            migrationBuilder.CreateTable(
                name: "HocPhiTronGois",
                columns: table => new
                {
                    HocPhiTronGoiId = table.Column<Guid>(nullable: false),
                    HocVienId = table.Column<Guid>(nullable: false),
                    HocPhi = table.Column<double>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhiTronGois", x => x.HocPhiTronGoiId);
                    table.ForeignKey(
                        name: "FK_HocPhiTronGois_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e553a1af-499b-43b1-82b2-0760c051e02d", "0a53ce33-2b27-4737-a611-faaae48fa253", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HocPhiTronGois_HocVienId",
                table: "HocPhiTronGois",
                column: "HocVienId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocPhiTronGois");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e553a1af-499b-43b1-82b2-0760c051e02d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20b49ec8-92a7-445e-95c0-8ef120a44524", "4d3d1a0e-2907-4770-9210-0e3ccf5697e2", "Admin", "ADMIN" });
        }
    }
}

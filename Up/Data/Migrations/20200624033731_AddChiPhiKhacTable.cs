using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddChiPhiKhacTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e73411f5-a39c-4c96-b7ee-e79478f87841");

            migrationBuilder.CreateTable(
                name: "ChiPhiKhacs",
                columns: table => new
                {
                    ChiPhiKhacId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiPhiKhacs", x => x.ChiPhiKhacId);
                });

            migrationBuilder.CreateTable(
                name: "ChiPhiKhac_Gias",
                columns: table => new
                {
                    ChiPhiKhac_GiaId = table.Column<Guid>(nullable: false),
                    ChiPhiKhacId = table.Column<Guid>(nullable: false),
                    Gia = table.Column<double>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiPhiKhac_Gias", x => x.ChiPhiKhac_GiaId);
                    table.ForeignKey(
                        name: "FK_ChiPhiKhac_Gias_ChiPhiKhacs_ChiPhiKhacId",
                        column: x => x.ChiPhiKhacId,
                        principalTable: "ChiPhiKhacs",
                        principalColumn: "ChiPhiKhacId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "38573848-e6d7-482e-b9b9-cdd952edc076", "c111b86d-d615-4278-83ce-b6f76cddef71", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ChiPhiKhac_Gias_ChiPhiKhacId",
                table: "ChiPhiKhac_Gias",
                column: "ChiPhiKhacId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiPhiKhac_Gias");

            migrationBuilder.DropTable(
                name: "ChiPhiKhacs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38573848-e6d7-482e-b9b9-cdd952edc076");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e73411f5-a39c-4c96-b7ee-e79478f87841", "3f092f15-7c5b-4e97-a559-15ebcc792df4", "Admin", "ADMIN" });
        }
    }
}

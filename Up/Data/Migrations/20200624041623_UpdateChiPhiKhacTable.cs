using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateChiPhiKhacTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiPhiKhac_Gias");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38573848-e6d7-482e-b9b9-cdd952edc076");

            migrationBuilder.AddColumn<double>(
                name: "Gia",
                table: "ChiPhiKhacs",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayChiPhi",
                table: "ChiPhiKhacs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d903329-6f61-4401-b0b3-3b9c7463025e", "5783ecdb-600e-412e-bfc1-3c27afd328bb", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d903329-6f61-4401-b0b3-3b9c7463025e");

            migrationBuilder.DropColumn(
                name: "Gia",
                table: "ChiPhiKhacs");

            migrationBuilder.DropColumn(
                name: "NgayChiPhi",
                table: "ChiPhiKhacs");

            migrationBuilder.CreateTable(
                name: "ChiPhiKhac_Gias",
                columns: table => new
                {
                    ChiPhiKhac_GiaId = table.Column<Guid>(nullable: false),
                    ChiPhiKhacId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Gia = table.Column<double>(nullable: false)
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
    }
}

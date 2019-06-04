using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddChiPhiCODInh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0978c547-9136-4114-85be-f06cd54d6008");

            migrationBuilder.CreateTable(
                name: "ChiPhiCoDinhs",
                columns: table => new
                {
                    ChiPhiCoDinhId = table.Column<Guid>(nullable: false),
                    Gia = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiPhiCoDinhs", x => x.ChiPhiCoDinhId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54b7f55d-4e04-4c90-b637-1eb27105eb3f", "eb72d8c0-2bc0-4cf7-9408-c060f9d21ea0", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiPhiCoDinhs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54b7f55d-4e04-4c90-b637-1eb27105eb3f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0978c547-9136-4114-85be-f06cd54d6008", "9d5f7d6e-fe04-4159-88de-477a7a513c4e", "Admin", "ADMIN" });
        }
    }
}

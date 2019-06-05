using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddDisable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54b7f55d-4e04-4c90-b637-1eb27105eb3f");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "ChiPhiCoDinhs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "81fd8a33-2c04-40c4-8257-90169f8476c4", "895bbf62-13e4-44aa-b096-91fc978a9109", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81fd8a33-2c04-40c4-8257-90169f8476c4");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "ChiPhiCoDinhs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "54b7f55d-4e04-4c90-b637-1eb27105eb3f", "eb72d8c0-2bc0-4cf7-9408-c060f9d21ea0", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddItemUseasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d19477af-1dc0-4daf-b8f6-8fec745b49db");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e39025d4-59ff-45d0-827a-22f1b8955d2c", "d1370a0f-6987-4441-90d2-39acb14d1ee1", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e39025d4-59ff-45d0-827a-22f1b8955d2c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d19477af-1dc0-4daf-b8f6-8fec745b49db", "207cbcfc-84d3-4b31-b5cb-b880f27aa45f", "Admin", "ADMIN" });
        }
    }
}

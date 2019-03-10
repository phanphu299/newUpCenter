using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c68d5392-6f59-4e0b-a3b7-375cd3c3b7c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "815bc042-0bc6-4778-9cbd-8969c93d1967", "4483133a-bc59-480f-866f-c7cb14649687", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "815bc042-0bc6-4778-9cbd-8969c93d1967");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c68d5392-6f59-4e0b-a3b7-375cd3c3b7c2", "1c655bb4-bb68-4986-86ee-8560b6b81a49", "Admin", "ADMIN" });
        }
    }
}

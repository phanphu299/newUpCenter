using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41c4d88b-7869-4b0d-9a2a-20c5d2bfd84c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d02c21a-1c93-437d-8dfa-24055fdc2dda", "39814346-a219-4a3e-8e31-e3bd1c269ab3", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d02c21a-1c93-437d-8dfa-24055fdc2dda");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41c4d88b-7869-4b0d-9a2a-20c5d2bfd84c", "8f994ca9-a790-4c21-a61a-e232ee2ca77b", "Admin", "ADMIN" });
        }
    }
}

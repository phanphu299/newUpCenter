using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class Addadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1125aa5c-e1fb-4325-9352-2d5d4583de2c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8efd191-74a9-4a5d-b8af-94a31306d3e8", "775515d2-3bf5-4b27-918f-96e7567defb1", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8efd191-74a9-4a5d-b8af-94a31306d3e8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1125aa5c-e1fb-4325-9352-2d5d4583de2c", "71078112-3c49-46ff-8a2c-be6fc6cd1a2d", "Admin", "ADMIN" });
        }
    }
}

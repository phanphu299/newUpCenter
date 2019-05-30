using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class addroleagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e4b69bf-3e99-418c-80bd-34e06dd30cda");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0978c547-9136-4114-85be-f06cd54d6008", "9d5f7d6e-fe04-4159-88de-477a7a513c4e", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0978c547-9136-4114-85be-f06cd54d6008");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3e4b69bf-3e99-418c-80bd-34e06dd30cda", "5ece560a-75de-4319-b136-1d440cffe1fa", "Admin", "ADMIN" });
        }
    }
}

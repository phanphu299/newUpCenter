using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class thongke : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d984675-a447-4c25-9fa6-769e069485bb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6a0198ab-4497-4834-920a-4119a1fc7ce9", "cb352f0a-c828-4306-84f6-fc351318fb10", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a0198ab-4497-4834-920a-4119a1fc7ce9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d984675-a447-4c25-9fa6-769e069485bb", "dde3c0af-5eb3-4f73-aa8a-f4e1434835c5", "Admin", "ADMIN" });
        }
    }
}

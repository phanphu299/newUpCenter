using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddNo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "842a8d1b-d2a9-4776-8513-c9172a20d2c1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30f5dc72-f0f1-47c5-86aa-4eaac7fad20a", "b7017095-689e-42e3-92c7-cab9464789e4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30f5dc72-f0f1-47c5-86aa-4eaac7fad20a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "842a8d1b-d2a9-4776-8513-c9172a20d2c1", "bd32459e-2981-4cc7-979f-84db24073906", "Admin", "ADMIN" });
        }
    }
}

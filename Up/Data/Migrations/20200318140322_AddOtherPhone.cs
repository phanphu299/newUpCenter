using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddOtherPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8efd191-74a9-4a5d-b8af-94a31306d3e8");

            migrationBuilder.AddColumn<string>(
                name: "OtherPhone",
                table: "HocViens",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b6b7c84-4226-4925-96e2-001ce95e8cb7", "59609e18-dfc3-48d2-9d43-ffafaf95fb32", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b6b7c84-4226-4925-96e2-001ce95e8cb7");

            migrationBuilder.DropColumn(
                name: "OtherPhone",
                table: "HocViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8efd191-74a9-4a5d-b8af-94a31306d3e8", "775515d2-3bf5-4b27-918f-96e7567defb1", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class Addcolumnsohocvien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5929137-90d8-41af-98a8-01f93182df04");

            migrationBuilder.AddColumn<double>(
                name: "SoHocVien",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9f35e79-c66c-435e-8a25-f5949efa4cd1", "34ba7677-9ccc-477d-bfe4-f97404b71046", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9f35e79-c66c-435e-8a25-f5949efa4cd1");

            migrationBuilder.DropColumn(
                name: "SoHocVien",
                table: "ThongKe_ChiPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5929137-90d8-41af-98a8-01f93182df04", "e348775f-feb1-4d1f-8b1d-c23e08c34825", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class Addcolumndadong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8f694b2-ca97-4ab1-bcf7-aab069cf609c");

            migrationBuilder.AddColumn<bool>(
                name: "DaDong",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b754db24-e37a-4633-80b2-79b115e65616", "66d4354c-77eb-403d-8e41-ee464f210285", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b754db24-e37a-4633-80b2-79b115e65616");

            migrationBuilder.DropColumn(
                name: "DaDong",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8f694b2-ca97-4ab1-bcf7-aab069cf609c", "1df24833-00e9-4d71-89a3-c29255fd515a", "Admin", "ADMIN" });
        }
    }
}

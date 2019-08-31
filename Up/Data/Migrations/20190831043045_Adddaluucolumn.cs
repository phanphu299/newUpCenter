using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class Adddaluucolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b754db24-e37a-4633-80b2-79b115e65616");

            migrationBuilder.AddColumn<bool>(
                name: "DaLuu",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6452cb6-613a-461c-9115-c00b8a571a4d", "9610806b-356f-4944-9362-b9cf13db54d8", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6452cb6-613a-461c-9115-c00b8a571a4d");

            migrationBuilder.DropColumn(
                name: "DaLuu",
                table: "ThongKe_ChiPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b754db24-e37a-4633-80b2-79b115e65616", "66d4354c-77eb-403d-8e41-ee464f210285", "Admin", "ADMIN" });
        }
    }
}

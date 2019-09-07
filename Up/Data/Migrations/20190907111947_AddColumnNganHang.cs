using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddColumnNganHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "647f0318-102a-4660-b263-1bc785c76a14");

            migrationBuilder.AddColumn<string>(
                name: "NganHang",
                table: "GiaoViens",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12f9d080-935d-4898-9a76-2d55473f44d7", "7b1d18c2-380e-4f23-98c7-34ea65aa1418", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12f9d080-935d-4898-9a76-2d55473f44d7");

            migrationBuilder.DropColumn(
                name: "NganHang",
                table: "GiaoViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "647f0318-102a-4660-b263-1bc785c76a14", "eada20d7-f9a0-449c-a413-b2220d14cbc5", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddTronGoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6482056a-fa87-44c4-bd01-855c0b5e36c3");

            migrationBuilder.AddColumn<bool>(
                name: "TronGoi",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "528e245e-7c46-4a61-b355-51f587e0c185", "eb6b13e9-d28e-4d34-b7d9-bcd694c1e080", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528e245e-7c46-4a61-b355-51f587e0c185");

            migrationBuilder.DropColumn(
                name: "TronGoi",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6482056a-fa87-44c4-bd01-855c0b5e36c3", "a0978300-803e-45d1-bb3e-5699d30d6689", "Admin", "ADMIN" });
        }
    }
}

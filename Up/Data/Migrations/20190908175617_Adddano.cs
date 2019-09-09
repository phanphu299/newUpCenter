using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class Adddano : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d99a702e-f698-4341-8d03-c6a6f94386a2");

            migrationBuilder.AddColumn<bool>(
                name: "DaNo",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b397acb1-82d5-4f49-9a4d-f3dfc5241828", "a5d9d67d-2df1-4ea6-9524-c2ee9dd1d7c5", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b397acb1-82d5-4f49-9a4d-f3dfc5241828");

            migrationBuilder.DropColumn(
                name: "DaNo",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d99a702e-f698-4341-8d03-c6a6f94386a2", "312edc53-e8cd-4476-ae27-9a1ec1ebbe83", "Admin", "ADMIN" });
        }
    }
}

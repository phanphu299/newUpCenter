using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class RemoveNO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "704fa17d-fc45-437c-af4c-6c9458eff969");

            migrationBuilder.DropColumn(
                name: "No",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d99a702e-f698-4341-8d03-c6a6f94386a2", "312edc53-e8cd-4476-ae27-9a1ec1ebbe83", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d99a702e-f698-4341-8d03-c6a6f94386a2");

            migrationBuilder.AddColumn<double>(
                name: "No",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "704fa17d-fc45-437c-af4c-6c9458eff969", "80495d2f-0a63-4660-a8a2-2325e7924993", "Admin", "ADMIN" });
        }
    }
}

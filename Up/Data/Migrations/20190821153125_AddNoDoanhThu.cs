using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddNoDoanhThu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2aac3505-2556-4263-9c07-0f540307d122");

            migrationBuilder.AddColumn<double>(
                name: "No",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9512c9ed-d799-4fdb-821d-92d7a486296f", "1e74dbec-e554-43c8-870b-1a34dd497f58", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9512c9ed-d799-4fdb-821d-92d7a486296f");

            migrationBuilder.DropColumn(
                name: "No",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2aac3505-2556-4263-9c07-0f540307d122", "0d9bba62-e9d0-41cc-9545-a52a49d9a3fd", "Admin", "ADMIN" });
        }
    }
}

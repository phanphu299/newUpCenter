using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateTableDoanhThu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b18394c-0888-475a-ae42-6c471d5ed60f");

            migrationBuilder.AddColumn<double>(
                name: "Bonus",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KhuyenMai",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Minus",
                table: "ThongKe_DoanhThuHocPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6eb504c9-49a7-408c-9c6d-e8dc972210a7", "22af6bc7-3119-4f6a-bd30-70cda769bf8d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6eb504c9-49a7-408c-9c6d-e8dc972210a7");

            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.DropColumn(
                name: "KhuyenMai",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.DropColumn(
                name: "Minus",
                table: "ThongKe_DoanhThuHocPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9b18394c-0888-475a-ae42-6c471d5ed60f", "331ca43d-47fc-4ca5-abe8-a4a7a19046df", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class Addnewcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "182fc24d-d69c-4d13-8b3a-618142e6e022");

            migrationBuilder.AddColumn<double>(
                name: "DailySalary",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MucHoaHong",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "NgayLamViec",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Salary_Expense",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "SoNgayLam",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SoNgayLamVoSau",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SoNgayNghi",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TeachingRate",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TutoringRate",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "647f0318-102a-4660-b263-1bc785c76a14", "eada20d7-f9a0-449c-a413-b2220d14cbc5", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "647f0318-102a-4660-b263-1bc785c76a14");

            migrationBuilder.DropColumn(
                name: "DailySalary",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "MucHoaHong",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "NgayLamViec",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "Salary_Expense",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "SoNgayLam",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "SoNgayLamVoSau",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "SoNgayNghi",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "TeachingRate",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "TutoringRate",
                table: "ThongKe_ChiPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "182fc24d-d69c-4d13-8b3a-618142e6e022", "8a4759d5-4349-4dd8-b41b-79326b729dcb", "Admin", "ADMIN" });
        }
    }
}

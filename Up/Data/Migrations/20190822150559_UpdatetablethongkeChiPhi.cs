using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdatetablethongkeChiPhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c018d74b-3a47-4869-a845-0f4d8d4508be");

            migrationBuilder.DropColumn(
                name: "TeachingRate",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "TutoringRate",
                table: "ThongKe_ChiPhis");

            migrationBuilder.AddColumn<double>(
                name: "SoGioDay",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SoGioKem",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f8f694b2-ca97-4ab1-bcf7-aab069cf609c", "1df24833-00e9-4d71-89a3-c29255fd515a", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8f694b2-ca97-4ab1-bcf7-aab069cf609c");

            migrationBuilder.DropColumn(
                name: "SoGioDay",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "SoGioKem",
                table: "ThongKe_ChiPhis");

            migrationBuilder.AddColumn<double>(
                name: "TeachingRate",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TutoringRate",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c018d74b-3a47-4869-a845-0f4d8d4508be", "4b2c8971-9728-4d69-b269-39eff996ac27", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddGhiChuColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c688e63f-027d-4389-8610-198afc919c14");

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20b49ec8-92a7-445e-95c0-8ef120a44524", "4d3d1a0e-2907-4770-9210-0e3ccf5697e2", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20b49ec8-92a7-445e-95c0-8ef120a44524");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "ThongKe_ChiPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c688e63f-027d-4389-8610-198afc919c14", "4db8ad81-fdbe-41a4-8400-0741dfb5b285", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateGioHocTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4894f75c-91e4-4d84-875f-a07a187ba62a");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "GioHocs",
                newName: "To");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "GioHocs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee972bb1-149d-460e-87fe-b5720ef2a599", "8c8626c8-94c4-47cd-8878-1465563522dd", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee972bb1-149d-460e-87fe-b5720ef2a599");

            migrationBuilder.DropColumn(
                name: "From",
                table: "GioHocs");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "GioHocs",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4894f75c-91e4-4d84-875f-a07a187ba62a", "b39a6525-be1e-42c9-89e1-c59a8d933792", "Admin", "ADMIN" });
        }
    }
}

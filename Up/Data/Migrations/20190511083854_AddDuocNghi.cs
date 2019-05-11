using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddDuocNghi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee972bb1-149d-460e-87fe-b5720ef2a599");

            migrationBuilder.AddColumn<bool>(
                name: "IsDuocNghi",
                table: "LopHoc_DiemDanhs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "41c4d88b-7869-4b0d-9a2a-20c5d2bfd84c", "8f994ca9-a790-4c21-a61a-e232ee2ca77b", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41c4d88b-7869-4b0d-9a2a-20c5d2bfd84c");

            migrationBuilder.DropColumn(
                name: "IsDuocNghi",
                table: "LopHoc_DiemDanhs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ee972bb1-149d-460e-87fe-b5720ef2a599", "8c8626c8-94c4-47cd-8878-1465563522dd", "Admin", "ADMIN" });
        }
    }
}

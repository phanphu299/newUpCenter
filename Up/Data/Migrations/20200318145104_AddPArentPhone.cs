using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddPArentPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b6b7c84-4226-4925-96e2-001ce95e8cb7");

            migrationBuilder.AddColumn<string>(
                name: "ParentPhone",
                table: "HocViens",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e73411f5-a39c-4c96-b7ee-e79478f87841", "3f092f15-7c5b-4e97-a559-15ebcc792df4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e73411f5-a39c-4c96-b7ee-e79478f87841");

            migrationBuilder.DropColumn(
                name: "ParentPhone",
                table: "HocViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3b6b7c84-4226-4925-96e2-001ce95e8cb7", "59609e18-dfc3-48d2-9d43-ffafaf95fb32", "Admin", "ADMIN" });
        }
    }
}

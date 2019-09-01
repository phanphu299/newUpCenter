using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddcolumnHoaHong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99f0ef37-ac0d-43e1-b03a-fc38f401ffb8");

            migrationBuilder.AddColumn<double>(
                name: "MucHoaHong",
                table: "GiaoViens",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5929137-90d8-41af-98a8-01f93182df04", "e348775f-feb1-4d1f-8b1d-c23e08c34825", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5929137-90d8-41af-98a8-01f93182df04");

            migrationBuilder.DropColumn(
                name: "MucHoaHong",
                table: "GiaoViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99f0ef37-ac0d-43e1-b03a-fc38f401ffb8", "8d5d0058-f546-4657-982a-49fcb6a80511", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class trigram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cdc6ff8-08c5-498d-b4d7-bc7134ddf7e7");

            migrationBuilder.AddColumn<string>(
                name: "Trigram",
                table: "HocViens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "HocPhiTronGois",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bad7234f-5ae2-43ba-b8a7-477ee67e5135", "b7f61128-4e6d-44f8-915f-35a97ccbf1af", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bad7234f-5ae2-43ba-b8a7-477ee67e5135");

            migrationBuilder.DropColumn(
                name: "Trigram",
                table: "HocViens");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "HocPhiTronGois");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7cdc6ff8-08c5-498d-b4d7-bc7134ddf7e7", "63e6dc9a-83a4-4e4d-8197-e68f499a5e01", "Admin", "ADMIN" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class RemoveSomeHocVienColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6ebbdd5-fd88-4bbe-9d7d-8f6093b94b3d");

            migrationBuilder.DropColumn(
                name: "IsAppend",
                table: "HocViens");

            migrationBuilder.DropColumn(
                name: "ParentFacebookAccount",
                table: "HocViens");

            migrationBuilder.DropColumn(
                name: "ParentPhone",
                table: "HocViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9b18394c-0888-475a-ae42-6c471d5ed60f", "331ca43d-47fc-4ca5-abe8-a4a7a19046df", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b18394c-0888-475a-ae42-6c471d5ed60f");

            migrationBuilder.AddColumn<bool>(
                name: "IsAppend",
                table: "HocViens",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ParentFacebookAccount",
                table: "HocViens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentPhone",
                table: "HocViens",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f6ebbdd5-fd88-4bbe-9d7d-8f6093b94b3d", "edfaac11-bd45-4919-9b01-22f81f4d5e5d", "Admin", "ADMIN" });
        }
    }
}

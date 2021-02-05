using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class ChangeNgaySinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63a054e2-1393-4457-ad77-824a57dea515");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "HocViens",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9990c3f-243c-42d5-ae05-ac8756682814", "3c0606b4-c163-472a-842f-2ab8fe0cc89f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9990c3f-243c-42d5-ae05-ac8756682814");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgaySinh",
                table: "HocViens",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63a054e2-1393-4457-ad77-824a57dea515", "25a5f382-a31f-4d54-9067-3875fd15ff93", "Admin", "ADMIN" });
        }
    }
}

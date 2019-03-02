using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateLopHoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb8e7cd8-0deb-4b82-a738-a9dc8b74d6d5");

            migrationBuilder.AlterColumn<Guid>(
                name: "HocPhiId",
                table: "LopHocs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1163f986-8c93-47cd-9fba-27c29bbba0dc", "df22065b-6d1e-474b-825b-37c9940bc161", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs",
                column: "HocPhiId",
                principalTable: "HocPhis",
                principalColumn: "HocPhiId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1163f986-8c93-47cd-9fba-27c29bbba0dc");

            migrationBuilder.AlterColumn<Guid>(
                name: "HocPhiId",
                table: "LopHocs",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb8e7cd8-0deb-4b82-a738-a9dc8b74d6d5", "ebeaea13-2038-4344-84a5-184277072ae4", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs",
                column: "HocPhiId",
                principalTable: "HocPhis",
                principalColumn: "HocPhiId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

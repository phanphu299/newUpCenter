using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class RemoveHocPhiId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs");

            migrationBuilder.DropIndex(
                name: "IX_LopHocs_HocPhiId",
                table: "LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12f9d080-935d-4898-9a76-2d55473f44d7");

            migrationBuilder.DropColumn(
                name: "HocPhiId",
                table: "LopHocs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "704fa17d-fc45-437c-af4c-6c9458eff969", "80495d2f-0a63-4660-a8a2-2325e7924993", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "704fa17d-fc45-437c-af4c-6c9458eff969");

            migrationBuilder.AddColumn<Guid>(
                name: "HocPhiId",
                table: "LopHocs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "12f9d080-935d-4898-9a76-2d55473f44d7", "7b1d18c2-380e-4f23-98c7-34ea65aa1418", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocs_HocPhiId",
                table: "LopHocs",
                column: "HocPhiId");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs",
                column: "HocPhiId",
                principalTable: "HocPhis",
                principalColumn: "HocPhiId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

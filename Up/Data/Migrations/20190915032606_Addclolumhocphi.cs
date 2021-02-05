using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class Addclolumhocphi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b9e75d1-56a8-48cc-81fe-2e03ff8b8e57");

            migrationBuilder.AddColumn<Guid>(
                name: "HocPhiId",
                table: "LopHocs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "77d51877-e22f-4044-a320-0e0da0b582a8", "3f4318f8-158b-4144-9c04-a4ba83483141", "Admin", "ADMIN" });

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: "77d51877-e22f-4044-a320-0e0da0b582a8");

            migrationBuilder.DropColumn(
                name: "HocPhiId",
                table: "LopHocs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b9e75d1-56a8-48cc-81fe-2e03ff8b8e57", "7aab0536-dc06-4cbd-8568-c27c76063387", "Admin", "ADMIN" });
        }
    }
}

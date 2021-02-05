using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class RemoveColumnGiaoVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocs_GiaoViens_GiaoVienId",
                table: "LopHocs");

            migrationBuilder.DropIndex(
                name: "IX_LopHocs_GiaoVienId",
                table: "LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9f35e79-c66c-435e-8a25-f5949efa4cd1");

            migrationBuilder.DropColumn(
                name: "GiaoVienId",
                table: "LopHocs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28932232-2a1d-4017-a2c4-170946944846", "2899fd05-011c-42b4-be4d-612c20a290d7", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28932232-2a1d-4017-a2c4-170946944846");

            migrationBuilder.AddColumn<Guid>(
                name: "GiaoVienId",
                table: "LopHocs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9f35e79-c66c-435e-8a25-f5949efa4cd1", "34ba7677-9ccc-477d-bfe4-f97404b71046", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocs_GiaoVienId",
                table: "LopHocs",
                column: "GiaoVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocs_GiaoViens_GiaoVienId",
                table: "LopHocs",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "GiaoVienId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

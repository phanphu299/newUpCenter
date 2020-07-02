using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddColumnDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocPhiTronGoi_LopHoc_HocPhiTronGois_HocPhiTronGoiId",
                table: "HocPhiTronGoi_LopHoc");

            migrationBuilder.DropForeignKey(
                name: "FK_HocPhiTronGoi_LopHoc_LopHocs_LopHocId",
                table: "HocPhiTronGoi_LopHoc");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HocPhiTronGoi_LopHoc",
                table: "HocPhiTronGoi_LopHoc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0288b36f-a846-4849-9ba9-7ed4dd421876");

            migrationBuilder.RenameTable(
                name: "HocPhiTronGoi_LopHoc",
                newName: "HocPhiTronGoi_LopHocs");

            migrationBuilder.RenameIndex(
                name: "IX_HocPhiTronGoi_LopHoc_LopHocId",
                table: "HocPhiTronGoi_LopHocs",
                newName: "IX_HocPhiTronGoi_LopHocs_LopHocId");

            migrationBuilder.RenameIndex(
                name: "IX_HocPhiTronGoi_LopHoc_HocPhiTronGoiId",
                table: "HocPhiTronGoi_LopHocs",
                newName: "IX_HocPhiTronGoi_LopHocs_HocPhiTronGoiId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "HocPhiTronGoi_LopHocs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "HocPhiTronGoi_LopHocs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_HocPhiTronGoi_LopHocs",
                table: "HocPhiTronGoi_LopHocs",
                column: "HocPhiTronGoi_LopHocId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6482056a-fa87-44c4-bd01-855c0b5e36c3", "a0978300-803e-45d1-bb3e-5699d30d6689", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhiTronGoi_LopHocs_HocPhiTronGois_HocPhiTronGoiId",
                table: "HocPhiTronGoi_LopHocs",
                column: "HocPhiTronGoiId",
                principalTable: "HocPhiTronGois",
                principalColumn: "HocPhiTronGoiId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhiTronGoi_LopHocs_LopHocs_LopHocId",
                table: "HocPhiTronGoi_LopHocs",
                column: "LopHocId",
                principalTable: "LopHocs",
                principalColumn: "LopHocId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocPhiTronGoi_LopHocs_HocPhiTronGois_HocPhiTronGoiId",
                table: "HocPhiTronGoi_LopHocs");

            migrationBuilder.DropForeignKey(
                name: "FK_HocPhiTronGoi_LopHocs_LopHocs_LopHocId",
                table: "HocPhiTronGoi_LopHocs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HocPhiTronGoi_LopHocs",
                table: "HocPhiTronGoi_LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6482056a-fa87-44c4-bd01-855c0b5e36c3");

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "HocPhiTronGoi_LopHocs");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "HocPhiTronGoi_LopHocs");

            migrationBuilder.RenameTable(
                name: "HocPhiTronGoi_LopHocs",
                newName: "HocPhiTronGoi_LopHoc");

            migrationBuilder.RenameIndex(
                name: "IX_HocPhiTronGoi_LopHocs_LopHocId",
                table: "HocPhiTronGoi_LopHoc",
                newName: "IX_HocPhiTronGoi_LopHoc_LopHocId");

            migrationBuilder.RenameIndex(
                name: "IX_HocPhiTronGoi_LopHocs_HocPhiTronGoiId",
                table: "HocPhiTronGoi_LopHoc",
                newName: "IX_HocPhiTronGoi_LopHoc_HocPhiTronGoiId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HocPhiTronGoi_LopHoc",
                table: "HocPhiTronGoi_LopHoc",
                column: "HocPhiTronGoi_LopHocId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0288b36f-a846-4849-9ba9-7ed4dd421876", "b2fc89ee-cbd5-4de4-b942-9006c2ff240a", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhiTronGoi_LopHoc_HocPhiTronGois_HocPhiTronGoiId",
                table: "HocPhiTronGoi_LopHoc",
                column: "HocPhiTronGoiId",
                principalTable: "HocPhiTronGois",
                principalColumn: "HocPhiTronGoiId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HocPhiTronGoi_LopHoc_LopHocs_LopHocId",
                table: "HocPhiTronGoi_LopHoc",
                column: "LopHocId",
                principalTable: "LopHocs",
                principalColumn: "LopHocId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

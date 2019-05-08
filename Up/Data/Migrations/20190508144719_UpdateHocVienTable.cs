using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateHocVienTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocViens_QuanHes_QuanHeId",
                table: "HocViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70dc13f3-6011-4752-baf0-3e230bc27bbf");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuanHeId",
                table: "HocViens",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4894f75c-91e4-4d84-875f-a07a187ba62a", "b39a6525-be1e-42c9-89e1-c59a8d933792", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_HocViens_QuanHes_QuanHeId",
                table: "HocViens",
                column: "QuanHeId",
                principalTable: "QuanHes",
                principalColumn: "QuanHeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocViens_QuanHes_QuanHeId",
                table: "HocViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4894f75c-91e4-4d84-875f-a07a187ba62a");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuanHeId",
                table: "HocViens",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "70dc13f3-6011-4752-baf0-3e230bc27bbf", "ee0b19a6-1240-4d7a-b9a8-44a597d4015d", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_HocViens_QuanHes_QuanHeId",
                table: "HocViens",
                column: "QuanHeId",
                principalTable: "QuanHes",
                principalColumn: "QuanHeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdateTableChiPhiThongKe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d903329-6f61-4401-b0b3-3b9c7463025e");

            migrationBuilder.AddColumn<Guid>(
                name: "ChiPhiKhacId",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c688e63f-027d-4389-8610-198afc919c14", "4db8ad81-fdbe-41a4-8400-0741dfb5b285", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_ChiPhis_ChiPhiKhacId",
                table: "ThongKe_ChiPhis",
                column: "ChiPhiKhacId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongKe_ChiPhis_ChiPhiKhacs_ChiPhiKhacId",
                table: "ThongKe_ChiPhis",
                column: "ChiPhiKhacId",
                principalTable: "ChiPhiKhacs",
                principalColumn: "ChiPhiKhacId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongKe_ChiPhis_ChiPhiKhacs_ChiPhiKhacId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropIndex(
                name: "IX_ThongKe_ChiPhis_ChiPhiKhacId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c688e63f-027d-4389-8610-198afc919c14");

            migrationBuilder.DropColumn(
                name: "ChiPhiKhacId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8d903329-6f61-4401-b0b3-3b9c7463025e", "5783ecdb-600e-412e-bfc1-3c27afd328bb", "Admin", "ADMIN" });
        }
    }
}

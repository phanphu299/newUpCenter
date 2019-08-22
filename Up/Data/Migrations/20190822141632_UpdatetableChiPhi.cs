using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class UpdatetableChiPhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9512c9ed-d799-4fdb-821d-92d7a486296f");

            migrationBuilder.AddColumn<double>(
                name: "Bonus",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "ChiPhiCoDinhId",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Minus",
                table: "ThongKe_ChiPhis",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "NhanVienId",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TeachingRate",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TutoringRate",
                table: "ThongKe_ChiPhis",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c018d74b-3a47-4869-a845-0f4d8d4508be", "4b2c8971-9728-4d69-b269-39eff996ac27", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_ChiPhis_ChiPhiCoDinhId",
                table: "ThongKe_ChiPhis",
                column: "ChiPhiCoDinhId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongKe_ChiPhis_NhanVienId",
                table: "ThongKe_ChiPhis",
                column: "NhanVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongKe_ChiPhis_ChiPhiCoDinhs_ChiPhiCoDinhId",
                table: "ThongKe_ChiPhis",
                column: "ChiPhiCoDinhId",
                principalTable: "ChiPhiCoDinhs",
                principalColumn: "ChiPhiCoDinhId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThongKe_ChiPhis_GiaoViens_NhanVienId",
                table: "ThongKe_ChiPhis",
                column: "NhanVienId",
                principalTable: "GiaoViens",
                principalColumn: "GiaoVienId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongKe_ChiPhis_ChiPhiCoDinhs_ChiPhiCoDinhId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongKe_ChiPhis_GiaoViens_NhanVienId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropIndex(
                name: "IX_ThongKe_ChiPhis_ChiPhiCoDinhId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropIndex(
                name: "IX_ThongKe_ChiPhis_NhanVienId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c018d74b-3a47-4869-a845-0f4d8d4508be");

            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "ChiPhiCoDinhId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "Minus",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "NhanVienId",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "TeachingRate",
                table: "ThongKe_ChiPhis");

            migrationBuilder.DropColumn(
                name: "TutoringRate",
                table: "ThongKe_ChiPhis");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9512c9ed-d799-4fdb-821d-92d7a486296f", "1e74dbec-e554-43c8-870b-1a34dd497f58", "Admin", "ADMIN" });
        }
    }
}

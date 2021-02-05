using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddNgayLamViecTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28932232-2a1d-4017-a2c4-170946944846");

            migrationBuilder.AddColumn<byte>(
                name: "Order",
                table: "LoaiGiaoViens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDau",
                table: "GiaoViens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetThuc",
                table: "GiaoViens",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NgayLamViecId",
                table: "GiaoViens",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "NgayLamViecs",
                columns: table => new
                {
                    NgayLamViecId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgayLamViecs", x => x.NgayLamViecId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "182fc24d-d69c-4d13-8b3a-618142e6e022", "8a4759d5-4349-4dd8-b41b-79326b729dcb", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_GiaoViens_NgayLamViecId",
                table: "GiaoViens",
                column: "NgayLamViecId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoViens_NgayLamViecs_NgayLamViecId",
                table: "GiaoViens",
                column: "NgayLamViecId",
                principalTable: "NgayLamViecs",
                principalColumn: "NgayLamViecId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoViens_NgayLamViecs_NgayLamViecId",
                table: "GiaoViens");

            migrationBuilder.DropTable(
                name: "NgayLamViecs");

            migrationBuilder.DropIndex(
                name: "IX_GiaoViens_NgayLamViecId",
                table: "GiaoViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "182fc24d-d69c-4d13-8b3a-618142e6e022");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "LoaiGiaoViens");

            migrationBuilder.DropColumn(
                name: "NgayBatDau",
                table: "GiaoViens");

            migrationBuilder.DropColumn(
                name: "NgayKetThuc",
                table: "GiaoViens");

            migrationBuilder.DropColumn(
                name: "NgayLamViecId",
                table: "GiaoViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28932232-2a1d-4017-a2c4-170946944846", "2899fd05-011c-42b4-be4d-612c20a290d7", "Admin", "ADMIN" });
        }
    }
}

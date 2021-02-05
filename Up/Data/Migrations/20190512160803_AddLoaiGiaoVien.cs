using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddLoaiGiaoVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d02c21a-1c93-437d-8dfa-24055fdc2dda");

            migrationBuilder.AddColumn<Guid>(
                name: "LoaiGiaoVienId",
                table: "GiaoViens",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoaiGiaoViens",
                columns: table => new
                {
                    LoaiGiaoVienId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiGiaoViens", x => x.LoaiGiaoVienId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e43efa91-9af4-4484-9cef-366808342b2a", "7ea09ddf-797f-486b-ab5f-3144aa4ed869", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_GiaoViens_LoaiGiaoVienId",
                table: "GiaoViens",
                column: "LoaiGiaoVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId",
                table: "GiaoViens",
                column: "LoaiGiaoVienId",
                principalTable: "LoaiGiaoViens",
                principalColumn: "LoaiGiaoVienId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId",
                table: "GiaoViens");

            migrationBuilder.DropTable(
                name: "LoaiGiaoViens");

            migrationBuilder.DropIndex(
                name: "IX_GiaoViens_LoaiGiaoVienId",
                table: "GiaoViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e43efa91-9af4-4484-9cef-366808342b2a");

            migrationBuilder.DropColumn(
                name: "LoaiGiaoVienId",
                table: "GiaoViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0d02c21a-1c93-437d-8dfa-24055fdc2dda", "39814346-a219-4a3e-8e31-e3bd1c269ab3", "Admin", "ADMIN" });
        }
    }
}

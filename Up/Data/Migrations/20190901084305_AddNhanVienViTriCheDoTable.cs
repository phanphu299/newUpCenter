using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddNhanVienViTriCheDoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoViens_LoaiCheDos_LoaiCheDoId",
                table: "GiaoViens");

            migrationBuilder.DropForeignKey(
                name: "FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId",
                table: "GiaoViens");

            migrationBuilder.DropIndex(
                name: "IX_GiaoViens_LoaiCheDoId",
                table: "GiaoViens");

            migrationBuilder.DropIndex(
                name: "IX_GiaoViens_LoaiGiaoVienId",
                table: "GiaoViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6452cb6-613a-461c-9115-c00b8a571a4d");

            migrationBuilder.DropColumn(
                name: "LoaiCheDoId",
                table: "GiaoViens");

            migrationBuilder.DropColumn(
                name: "LoaiGiaoVienId",
                table: "GiaoViens");

            migrationBuilder.CreateTable(
                name: "NhanVien_ViTris",
                columns: table => new
                {
                    NhanVien_ViTriId = table.Column<Guid>(nullable: false),
                    NhanVienId = table.Column<Guid>(nullable: false),
                    ViTriId = table.Column<Guid>(nullable: false),
                    CheDoId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien_ViTris", x => x.NhanVien_ViTriId);
                    table.ForeignKey(
                        name: "FK_NhanVien_ViTris_LoaiCheDos_CheDoId",
                        column: x => x.CheDoId,
                        principalTable: "LoaiCheDos",
                        principalColumn: "LoaiCheDoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_ViTris_GiaoViens_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "GiaoViens",
                        principalColumn: "GiaoVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanVien_ViTris_LoaiGiaoViens_ViTriId",
                        column: x => x.ViTriId,
                        principalTable: "LoaiGiaoViens",
                        principalColumn: "LoaiGiaoVienId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99f0ef37-ac0d-43e1-b03a-fc38f401ffb8", "8d5d0058-f546-4657-982a-49fcb6a80511", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_ViTris_CheDoId",
                table: "NhanVien_ViTris",
                column: "CheDoId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_ViTris_NhanVienId",
                table: "NhanVien_ViTris",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_ViTris_ViTriId",
                table: "NhanVien_ViTris",
                column: "ViTriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhanVien_ViTris");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99f0ef37-ac0d-43e1-b03a-fc38f401ffb8");

            migrationBuilder.AddColumn<Guid>(
                name: "LoaiCheDoId",
                table: "GiaoViens",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LoaiGiaoVienId",
                table: "GiaoViens",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a6452cb6-613a-461c-9115-c00b8a571a4d", "9610806b-356f-4944-9362-b9cf13db54d8", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_GiaoViens_LoaiCheDoId",
                table: "GiaoViens",
                column: "LoaiCheDoId");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoViens_LoaiGiaoVienId",
                table: "GiaoViens",
                column: "LoaiGiaoVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoViens_LoaiCheDos_LoaiCheDoId",
                table: "GiaoViens",
                column: "LoaiCheDoId",
                principalTable: "LoaiCheDos",
                principalColumn: "LoaiCheDoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId",
                table: "GiaoViens",
                column: "LoaiGiaoVienId",
                principalTable: "LoaiGiaoViens",
                principalColumn: "LoaiGiaoVienId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

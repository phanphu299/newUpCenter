using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddLoaiCHeDo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea19b33d-a2ef-4919-b91f-efda405ac726");

            migrationBuilder.AddColumn<Guid>(
                name: "LoaiCheDoId",
                table: "GiaoViens",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "LoaiCheDos",
                columns: table => new
                {
                    LoaiCheDoId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiCheDos", x => x.LoaiCheDoId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fe37f8ed-9ce2-4297-8010-cf8c6dcca898", "d02b6f31-ae96-4eb6-a245-5adb032af371", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_GiaoViens_LoaiCheDoId",
                table: "GiaoViens",
                column: "LoaiCheDoId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoViens_LoaiCheDos_LoaiCheDoId",
                table: "GiaoViens",
                column: "LoaiCheDoId",
                principalTable: "LoaiCheDos",
                principalColumn: "LoaiCheDoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoViens_LoaiCheDos_LoaiCheDoId",
                table: "GiaoViens");

            migrationBuilder.DropTable(
                name: "LoaiCheDos");

            migrationBuilder.DropIndex(
                name: "IX_GiaoViens_LoaiCheDoId",
                table: "GiaoViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe37f8ed-9ce2-4297-8010-cf8c6dcca898");

            migrationBuilder.DropColumn(
                name: "LoaiCheDoId",
                table: "GiaoViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea19b33d-a2ef-4919-b91f-efda405ac726", "662855fc-aeef-4bb7-bf86-fdbdb3c4ea58", "Admin", "ADMIN" });
        }
    }
}

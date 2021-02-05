using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class updategiaovien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId",
                table: "GiaoViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e43efa91-9af4-4484-9cef-366808342b2a");

            migrationBuilder.AlterColumn<Guid>(
                name: "LoaiGiaoVienId",
                table: "GiaoViens",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d984675-a447-4c25-9fa6-769e069485bb", "dde3c0af-5eb3-4f73-aa8a-f4e1434835c5", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId",
                table: "GiaoViens",
                column: "LoaiGiaoVienId",
                principalTable: "LoaiGiaoViens",
                principalColumn: "LoaiGiaoVienId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId",
                table: "GiaoViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d984675-a447-4c25-9fa6-769e069485bb");

            migrationBuilder.AlterColumn<Guid>(
                name: "LoaiGiaoVienId",
                table: "GiaoViens",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e43efa91-9af4-4484-9cef-366808342b2a", "7ea09ddf-797f-486b-ab5f-3144aa4ed869", "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoViens_LoaiGiaoViens_LoaiGiaoVienId",
                table: "GiaoViens",
                column: "LoaiGiaoVienId",
                principalTable: "LoaiGiaoViens",
                principalColumn: "LoaiGiaoVienId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

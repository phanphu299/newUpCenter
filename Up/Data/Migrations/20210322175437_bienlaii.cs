using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class bienlaii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "528e245e-7c46-4a61-b355-51f587e0c185");

            migrationBuilder.AddColumn<string>(
                name: "CMND",
                table: "HocViens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "HocViens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "HocViens",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BienLais",
                columns: table => new
                {
                    BienLaiId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    HocVienId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: true),
                    HocPhi = table.Column<double>(nullable: false),
                    ThangHocPhi = table.Column<string>(nullable: true),
                    MaBienLai = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienLais", x => x.BienLaiId);
                    table.ForeignKey(
                        name: "FK_BienLais_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7cdc6ff8-08c5-498d-b4d7-bc7134ddf7e7", "63e6dc9a-83a4-4e4d-8197-e68f499a5e01", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_BienLais_HocVienId",
                table: "BienLais",
                column: "HocVienId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BienLais");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cdc6ff8-08c5-498d-b4d7-bc7134ddf7e7");

            migrationBuilder.DropColumn(
                name: "CMND",
                table: "HocViens");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "HocViens");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "HocViens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "528e245e-7c46-4a61-b355-51f587e0c185", "eb6b13e9-d28e-4d34-b7d9-bcd694c1e080", "Admin", "ADMIN" });
        }
    }
}

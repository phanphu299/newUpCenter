using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddGiaoVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbea46e5-3c05-41a9-9a72-31d769d5e19e");

            migrationBuilder.AddColumn<Guid>(
                name: "GiaoVienId",
                table: "LopHocs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GiaoViens",
                columns: table => new
                {
                    GiaoVienId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    TeachingRate = table.Column<double>(nullable: false),
                    TutoringRate = table.Column<double>(nullable: false),
                    BasicSalary = table.Column<double>(nullable: false),
                    FacebookAccount = table.Column<string>(nullable: true),
                    DiaChi = table.Column<string>(nullable: true),
                    InitialName = table.Column<string>(nullable: true),
                    CMND = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoViens", x => x.GiaoVienId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "94496989-732c-4a8d-a47a-646a4ec9fea5", "c7ac9f11-0c42-41fa-a039-6b9693eaf347", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocs_GiaoVienId",
                table: "LopHocs",
                column: "GiaoVienId");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocs_GiaoViens_GiaoVienId",
                table: "LopHocs",
                column: "GiaoVienId",
                principalTable: "GiaoViens",
                principalColumn: "GiaoVienId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocs_GiaoViens_GiaoVienId",
                table: "LopHocs");

            migrationBuilder.DropTable(
                name: "GiaoViens");

            migrationBuilder.DropIndex(
                name: "IX_LopHocs_GiaoVienId",
                table: "LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94496989-732c-4a8d-a47a-646a4ec9fea5");

            migrationBuilder.DropColumn(
                name: "GiaoVienId",
                table: "LopHocs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbea46e5-3c05-41a9-9a72-31d769d5e19e", "0a16779b-c249-48bc-9e5d-c5585f6aed23", "Admin", "ADMIN" });
        }
    }
}

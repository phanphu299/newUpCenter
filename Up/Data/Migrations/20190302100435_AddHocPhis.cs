using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class AddHocPhis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "823d908d-b8ae-48c9-8b86-21051161a68f");

            migrationBuilder.AddColumn<Guid>(
                name: "HocPhiId",
                table: "LopHocs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HocPhis",
                columns: table => new
                {
                    HocPhiId = table.Column<Guid>(nullable: false),
                    Gia = table.Column<double>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhis", x => x.HocPhiId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cb8e7cd8-0deb-4b82-a738-a9dc8b74d6d5", "ebeaea13-2038-4344-84a5-184277072ae4", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocs_HocPhiId",
                table: "LopHocs",
                column: "HocPhiId");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs",
                column: "HocPhiId",
                principalTable: "HocPhis",
                principalColumn: "HocPhiId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocs_HocPhis_HocPhiId",
                table: "LopHocs");

            migrationBuilder.DropTable(
                name: "HocPhis");

            migrationBuilder.DropIndex(
                name: "IX_LopHocs_HocPhiId",
                table: "LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb8e7cd8-0deb-4b82-a738-a9dc8b74d6d5");

            migrationBuilder.DropColumn(
                name: "HocPhiId",
                table: "LopHocs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "823d908d-b8ae-48c9-8b86-21051161a68f", "df9ff11f-1092-4b30-90bc-f6a6f5dea59c", "Admin", "ADMIN" });
        }
    }
}

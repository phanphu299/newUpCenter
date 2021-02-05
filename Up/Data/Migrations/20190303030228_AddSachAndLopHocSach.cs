using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddSachAndLopHocSach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1163f986-8c93-47cd-9fba-27c29bbba0dc");

            migrationBuilder.CreateTable(
                name: "Sachs",
                columns: table => new
                {
                    SachId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Gia = table.Column<double>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sachs", x => x.SachId);
                });

            migrationBuilder.CreateTable(
                name: "LopHoc_Sachs",
                columns: table => new
                {
                    LopHoc_SachId = table.Column<Guid>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    SachId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc_Sachs", x => x.LopHoc_SachId);
                    table.ForeignKey(
                        name: "FK_LopHoc_Sachs_LopHocs_LopHocId",
                        column: x => x.LopHocId,
                        principalTable: "LopHocs",
                        principalColumn: "LopHocId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHoc_Sachs_Sachs_SachId",
                        column: x => x.SachId,
                        principalTable: "Sachs",
                        principalColumn: "SachId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c68d5392-6f59-4e0b-a3b7-375cd3c3b7c2", "1c655bb4-bb68-4986-86ee-8560b6b81a49", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_Sachs_LopHocId",
                table: "LopHoc_Sachs",
                column: "LopHocId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_Sachs_SachId",
                table: "LopHoc_Sachs",
                column: "SachId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LopHoc_Sachs");

            migrationBuilder.DropTable(
                name: "Sachs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c68d5392-6f59-4e0b-a3b7-375cd3c3b7c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1163f986-8c93-47cd-9fba-27c29bbba0dc", "df22065b-6d1e-474b-825b-37c9940bc161", "Admin", "ADMIN" });
        }
    }
}

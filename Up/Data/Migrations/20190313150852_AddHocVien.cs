using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddHocVien : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "815bc042-0bc6-4778-9cbd-8969c93d1967");

            migrationBuilder.CreateTable(
                name: "HocViens",
                columns: table => new
                {
                    HocVienId = table.Column<Guid>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    FacebookAccount = table.Column<string>(nullable: true),
                    ParentFullName = table.Column<string>(nullable: true),
                    ParentPhone = table.Column<string>(nullable: true),
                    QuanHeId = table.Column<Guid>(nullable: false),
                    ParentFacebookAccount = table.Column<string>(nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: false),
                    EnglishName = table.Column<string>(nullable: true),
                    IsAppend = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocViens", x => x.HocVienId);
                    table.ForeignKey(
                        name: "FK_HocViens_QuanHes_QuanHeId",
                        column: x => x.QuanHeId,
                        principalTable: "QuanHes",
                        principalColumn: "QuanHeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbea46e5-3c05-41a9-9a72-31d769d5e19e", "0a16779b-c249-48bc-9e5d-c5585f6aed23", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_HocViens_QuanHeId",
                table: "HocViens",
                column: "QuanHeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HocViens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbea46e5-3c05-41a9-9a72-31d769d5e19e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "815bc042-0bc6-4778-9cbd-8969c93d1967", "4483133a-bc59-480f-866f-c7cb14649687", "Admin", "ADMIN" });
        }
    }
}

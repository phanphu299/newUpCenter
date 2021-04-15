using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class ChallengeResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb9ad74b-63e4-4ec7-9322-f3512ad9f743");

            migrationBuilder.CreateTable(
                name: "ChallengeResults",
                columns: table => new
                {
                    ChallengeResultId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    HocVienId = table.Column<Guid>(nullable: false),
                    ThuThachId = table.Column<Guid>(nullable: false),
                    LanThi = table.Column<int>(nullable: false),
                    IsPass = table.Column<bool>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeResults", x => x.ChallengeResultId);
                    table.ForeignKey(
                        name: "FK_ChallengeResults_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeResults_ThuThachs_ThuThachId",
                        column: x => x.ThuThachId,
                        principalTable: "ThuThachs",
                        principalColumn: "ThuThachId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67e4c8da-7ac3-42f8-bcfe-bcc597b9b94e", "6bc34398-5060-47db-a6ae-a68c69276b40", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeResults_HocVienId",
                table: "ChallengeResults",
                column: "HocVienId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeResults_ThuThachId",
                table: "ChallengeResults",
                column: "ThuThachId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeResults");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67e4c8da-7ac3-42f8-bcfe-bcc597b9b94e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb9ad74b-63e4-4ec7-9322-f3512ad9f743", "b58d4af6-ced2-4791-ba07-54af8efa2ff0", "Admin", "ADMIN" });
        }
    }
}

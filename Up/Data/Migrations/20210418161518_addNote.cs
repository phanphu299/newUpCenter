using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class addNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67e4c8da-7ac3-42f8-bcfe-bcc597b9b94e");

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    HocVienId = table.Column<Guid>(nullable: false),
                    GhiChu = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_HocViens_HocVienId",
                        column: x => x.HocVienId,
                        principalTable: "HocViens",
                        principalColumn: "HocVienId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a8c820a-32bf-4594-b428-c76c1ac0119a", "ed7e034c-a8b7-4b5d-9b26-d1a98e3ca0df", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_HocVienId",
                table: "Notes",
                column: "HocVienId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a8c820a-32bf-4594-b428-c76c1ac0119a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "67e4c8da-7ac3-42f8-bcfe-bcc597b9b94e", "6bc34398-5060-47db-a6ae-a68c69276b40", "Admin", "ADMIN" });
        }
    }
}

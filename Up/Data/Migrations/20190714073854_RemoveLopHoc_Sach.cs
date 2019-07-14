using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class RemoveLopHoc_Sach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LopHoc_Sachs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9990c3f-243c-42d5-ae05-ac8756682814");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea19b33d-a2ef-4919-b91f-efda405ac726", "662855fc-aeef-4bb7-bf86-fdbdb3c4ea58", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea19b33d-a2ef-4919-b91f-efda405ac726");

            migrationBuilder.CreateTable(
                name: "LopHoc_Sachs",
                columns: table => new
                {
                    LopHoc_SachId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    LopHocId = table.Column<Guid>(nullable: false),
                    SachId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
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
                values: new object[] { "e9990c3f-243c-42d5-ae05-ac8756682814", "3c0606b4-c163-472a-842f-2ab8fe0cc89f", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_Sachs_LopHocId",
                table: "LopHoc_Sachs",
                column: "LopHocId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_Sachs_SachId",
                table: "LopHoc_Sachs",
                column: "SachId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddLopHocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "312d2117-8888-4dbb-ad55-0c2d5745c51d");

            migrationBuilder.CreateTable(
                name: "LopHocs",
                columns: table => new
                {
                    LopHocId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    IsGraduated = table.Column<bool>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    KhoaHocId = table.Column<Guid>(nullable: false),
                    NgayHocId = table.Column<Guid>(nullable: false),
                    GioHocId = table.Column<Guid>(nullable: false),
                    NgayKhaiGiang = table.Column<DateTime>(nullable: false),
                    NgayKetThuc = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHocs", x => x.LopHocId);
                    table.ForeignKey(
                        name: "FK_LopHocs_GioHocs_GioHocId",
                        column: x => x.GioHocId,
                        principalTable: "GioHocs",
                        principalColumn: "GioHocId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHocs_KhoaHocs_KhoaHocId",
                        column: x => x.KhoaHocId,
                        principalTable: "KhoaHocs",
                        principalColumn: "KhoaHocId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LopHocs_NgayHocs_NgayHocId",
                        column: x => x.NgayHocId,
                        principalTable: "NgayHocs",
                        principalColumn: "NgayHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "369bdd73-75db-4e19-9a3d-b45373ebcaef", "454c8836-1099-4093-8f6f-dbffd9699ba3", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_LopHocs_GioHocId",
                table: "LopHocs",
                column: "GioHocId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocs_KhoaHocId",
                table: "LopHocs",
                column: "KhoaHocId");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocs_NgayHocId",
                table: "LopHocs",
                column: "NgayHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LopHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "369bdd73-75db-4e19-9a3d-b45373ebcaef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "312d2117-8888-4dbb-ad55-0c2d5745c51d", "351b6f70-1131-4dfc-bc7c-eaf3999d31c3", "Admin", "ADMIN" });
        }
    }
}

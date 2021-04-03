using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Up.Data.Migrations
{
    public partial class challenge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bad7234f-5ae2-43ba-b8a7-477ee67e5135");

            migrationBuilder.CreateTable(
                name: "ThuThachs",
                columns: table => new
                {
                    ThuThachId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    KhoaHocId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SoCauHoi = table.Column<int>(nullable: false),
                    ThoiGianLamBai = table.Column<int>(nullable: false),
                    MinGrade = table.Column<int>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuThachs", x => x.ThuThachId);
                    table.ForeignKey(
                        name: "FK_ThuThachs_KhoaHocs_KhoaHocId",
                        column: x => x.KhoaHocId,
                        principalTable: "KhoaHocs",
                        principalColumn: "KhoaHocId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CauHois",
                columns: table => new
                {
                    CauHoiId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    ThuThachId = table.Column<Guid>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    STT = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHois", x => x.CauHoiId);
                    table.ForeignKey(
                        name: "FK_CauHois_ThuThachs_ThuThachId",
                        column: x => x.ThuThachId,
                        principalTable: "ThuThachs",
                        principalColumn: "ThuThachId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DapAns",
                columns: table => new
                {
                    DapAnId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    CauHoiId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsTrue = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DapAns", x => x.DapAnId);
                    table.ForeignKey(
                        name: "FK_DapAns_CauHois_CauHoiId",
                        column: x => x.CauHoiId,
                        principalTable: "CauHois",
                        principalColumn: "CauHoiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb9ad74b-63e4-4ec7-9322-f3512ad9f743", "b58d4af6-ced2-4791-ba07-54af8efa2ff0", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_CauHois_ThuThachId",
                table: "CauHois",
                column: "ThuThachId");

            migrationBuilder.CreateIndex(
                name: "IX_DapAns_CauHoiId",
                table: "DapAns",
                column: "CauHoiId");

            migrationBuilder.CreateIndex(
                name: "IX_ThuThachs_KhoaHocId",
                table: "ThuThachs",
                column: "KhoaHocId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DapAns");

            migrationBuilder.DropTable(
                name: "CauHois");

            migrationBuilder.DropTable(
                name: "ThuThachs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb9ad74b-63e4-4ec7-9322-f3512ad9f743");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bad7234f-5ae2-43ba-b8a7-477ee67e5135", "b7f61128-4e6d-44f8-915f-35a97ccbf1af", "Admin", "ADMIN" });
        }
    }
}

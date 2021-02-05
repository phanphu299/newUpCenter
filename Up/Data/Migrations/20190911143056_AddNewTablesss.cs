using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddNewTablesss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b397acb1-82d5-4f49-9a4d-f3dfc5241828");

            migrationBuilder.CreateTable(
                name: "Quyens",
                columns: table => new
                {
                    QuyenId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quyens", x => x.QuyenId);
                });

            migrationBuilder.CreateTable(
                name: "Quyen_Roles",
                columns: table => new
                {
                    Quyen_RoleId = table.Column<Guid>(nullable: false),
                    QuyenId = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quyen_Roles", x => x.Quyen_RoleId);
                    table.ForeignKey(
                        name: "FK_Quyen_Roles_Quyens_QuyenId",
                        column: x => x.QuyenId,
                        principalTable: "Quyens",
                        principalColumn: "QuyenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b9e75d1-56a8-48cc-81fe-2e03ff8b8e57", "7aab0536-dc06-4cbd-8568-c27c76063387", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Quyen_Roles_QuyenId",
                table: "Quyen_Roles",
                column: "QuyenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quyen_Roles");

            migrationBuilder.DropTable(
                name: "Quyens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b9e75d1-56a8-48cc-81fe-2e03ff8b8e57");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b397acb1-82d5-4f49-9a4d-f3dfc5241828", "a5d9d67d-2df1-4ea6-9524-c2ee9dd1d7c5", "Admin", "ADMIN" });
        }
    }
}

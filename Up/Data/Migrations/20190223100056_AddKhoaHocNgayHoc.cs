using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Up.Data.Migrations
{
    public partial class AddKhoaHocNgayHoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6126d9bc-e832-4068-945b-f0c84da04d11");

            migrationBuilder.CreateTable(
                name: "GioHocs",
                columns: table => new
                {
                    GioHocId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHocs", x => x.GioHocId);
                });

            migrationBuilder.CreateTable(
                name: "NgayHocs",
                columns: table => new
                {
                    NgayHocId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDisabled = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NgayHocs", x => x.NgayHocId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "312d2117-8888-4dbb-ad55-0c2d5745c51d", "351b6f70-1131-4dfc-bc7c-eaf3999d31c3", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GioHocs");

            migrationBuilder.DropTable(
                name: "NgayHocs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "312d2117-8888-4dbb-ad55-0c2d5745c51d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6126d9bc-e832-4068-945b-f0c84da04d11", "6a36462f-07b3-4501-b5dc-03af6f792225", "Admin", "ADMIN" });
        }
    }
}

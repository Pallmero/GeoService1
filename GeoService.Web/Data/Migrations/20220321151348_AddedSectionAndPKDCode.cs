using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoService.Web.Data.Migrations
{
    public partial class AddedSectionAndPKDCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PKDCodeId",
                table: "Objects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PKDCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PKDSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PKDCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PKDCodes_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Objects_PKDCodeId",
                table: "Objects",
                column: "PKDCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_PKDCodes_SectionId",
                table: "PKDCodes",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_PKDCodes_PKDCodeId",
                table: "Objects",
                column: "PKDCodeId",
                principalTable: "PKDCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_PKDCodes_PKDCodeId",
                table: "Objects");

            migrationBuilder.DropTable(
                name: "PKDCodes");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Objects_PKDCodeId",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "PKDCodeId",
                table: "Objects");
        }
    }
}

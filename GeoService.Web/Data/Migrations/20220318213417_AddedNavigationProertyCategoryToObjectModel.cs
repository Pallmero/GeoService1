using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoService.Web.Data.Migrations
{
    public partial class AddedNavigationProertyCategoryToObjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Objects_CategoryId",
                table: "Objects",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objects_Categories_CategoryId",
                table: "Objects",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objects_Categories_CategoryId",
                table: "Objects");

            migrationBuilder.DropIndex(
                name: "IX_Objects_CategoryId",
                table: "Objects");
        }
    }
}

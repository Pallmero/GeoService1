using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoService.Web.Data.Migrations
{
    public partial class AddedLongitudeAndLatitudeToObjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Objects",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Objects",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Objects");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Objects");
        }
    }
}

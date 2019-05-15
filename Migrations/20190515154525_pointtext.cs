using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class pointtext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PointTextEn",
                table: "Point",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PointTextFr",
                table: "Point",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointTextEn",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "PointTextFr",
                table: "Point");
        }
    }
}

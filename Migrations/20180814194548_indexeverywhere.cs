using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class indexeverywhere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Strata",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Measure",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "LifeCourse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "IndicatorGroup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Indicator",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Activity",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "LifeCourse");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "IndicatorGroup");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Indicator");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Activity");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class didstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StrataPopulationFr",
                table: "Strata",
                newName: "StrataPopulationTitleFragmentFr");

            migrationBuilder.RenameColumn(
                name: "StrataPopulationEn",
                table: "Strata",
                newName: "StrataPopulationTitleFragmentEn");

            migrationBuilder.RenameColumn(
                name: "MeasureSourceEn",
                table: "Measure",
                newName: "MeasureUnitShortEn");

            migrationBuilder.RenameColumn(
                name: "MeasurePopulationFr",
                table: "Measure",
                newName: "MeasureUnitLongEn");

            migrationBuilder.RenameColumn(
                name: "MeasurePopulationEn",
                table: "Measure",
                newName: "MeasureSourceShortEn");

            migrationBuilder.RenameColumn(
                name: "MeasureNameFr",
                table: "Measure",
                newName: "MeasureSourceLongEn");

            migrationBuilder.RenameColumn(
                name: "MeasureNameEn",
                table: "Measure",
                newName: "MeasurePopulationGroupFr");

            migrationBuilder.AddColumn<string>(
                name: "MeasureNameDataToolEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureNameDataToolFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureNameIndexEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureNameIndexFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasurePopulationGroupEn",
                table: "Measure",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasureNameDataToolEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureNameDataToolFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureNameIndexEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureNameIndexFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasurePopulationGroupEn",
                table: "Measure");

            migrationBuilder.RenameColumn(
                name: "StrataPopulationTitleFragmentFr",
                table: "Strata",
                newName: "StrataPopulationFr");

            migrationBuilder.RenameColumn(
                name: "StrataPopulationTitleFragmentEn",
                table: "Strata",
                newName: "StrataPopulationEn");

            migrationBuilder.RenameColumn(
                name: "MeasureUnitShortEn",
                table: "Measure",
                newName: "MeasureSourceEn");

            migrationBuilder.RenameColumn(
                name: "MeasureUnitLongEn",
                table: "Measure",
                newName: "MeasurePopulationFr");

            migrationBuilder.RenameColumn(
                name: "MeasureSourceShortEn",
                table: "Measure",
                newName: "MeasurePopulationEn");

            migrationBuilder.RenameColumn(
                name: "MeasureSourceLongEn",
                table: "Measure",
                newName: "MeasureNameFr");

            migrationBuilder.RenameColumn(
                name: "MeasurePopulationGroupFr",
                table: "Measure",
                newName: "MeasureNameEn");
        }
    }
}

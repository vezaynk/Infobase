using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class dfdfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MeasureUnitFr",
                table: "Measure",
                newName: "MeasureUnitShortFr");

            migrationBuilder.RenameColumn(
                name: "MeasureUnitEn",
                table: "Measure",
                newName: "MeasureUnitLongFr");

            migrationBuilder.RenameColumn(
                name: "MeasureSourceFr",
                table: "Measure",
                newName: "MeasureSourceShortFr");

            migrationBuilder.AddColumn<string>(
                name: "MeasureSourceLongFr",
                table: "Measure",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasureSourceLongFr",
                table: "Measure");

            migrationBuilder.RenameColumn(
                name: "MeasureUnitShortFr",
                table: "Measure",
                newName: "MeasureUnitFr");

            migrationBuilder.RenameColumn(
                name: "MeasureUnitLongFr",
                table: "Measure",
                newName: "MeasureUnitEn");

            migrationBuilder.RenameColumn(
                name: "MeasureSourceShortFr",
                table: "Measure",
                newName: "MeasureSourceFr");
        }
    }
}

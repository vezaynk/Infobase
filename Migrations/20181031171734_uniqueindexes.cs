using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class uniqueindexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Strata_Index",
                table: "Strata",
                column: "Index");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Point_Index",
                table: "Point",
                column: "Index");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Measure_Index",
                table: "Measure",
                column: "Index");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_LifeCourse_Index",
                table: "LifeCourse",
                column: "Index");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_IndicatorGroup_Index",
                table: "IndicatorGroup",
                column: "Index");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Indicator_Index",
                table: "Indicator",
                column: "Index");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Activity_Index",
                table: "Activity",
                column: "Index");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Strata_Index",
                table: "Strata");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Point_Index",
                table: "Point");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Measure_Index",
                table: "Measure");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_LifeCourse_Index",
                table: "LifeCourse");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_IndicatorGroup_Index",
                table: "IndicatorGroup");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Indicator_Index",
                table: "Indicator");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Activity_Index",
                table: "Activity");
        }
    }
}

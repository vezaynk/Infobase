using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class defaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultStrataId",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultIndicatorId",
                table: "LifeCourse",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultLifeCourseId",
                table: "IndicatorGroup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultMeasureId",
                table: "Indicator",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultIndicatorGroupId",
                table: "Activity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measure_DefaultStrataId",
                table: "Measure",
                column: "DefaultStrataId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCourse_DefaultIndicatorId",
                table: "LifeCourse",
                column: "DefaultIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorGroup_DefaultLifeCourseId",
                table: "IndicatorGroup",
                column: "DefaultLifeCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicator_DefaultMeasureId",
                table: "Indicator",
                column: "DefaultMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_DefaultIndicatorGroupId",
                table: "Activity",
                column: "DefaultIndicatorGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_IndicatorGroup_DefaultIndicatorGroupId",
                table: "Activity",
                column: "DefaultIndicatorGroupId",
                principalTable: "IndicatorGroup",
                principalColumn: "IndicatorGroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Indicator_Measure_DefaultMeasureId",
                table: "Indicator",
                column: "DefaultMeasureId",
                principalTable: "Measure",
                principalColumn: "MeasureId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IndicatorGroup_LifeCourse_DefaultLifeCourseId",
                table: "IndicatorGroup",
                column: "DefaultLifeCourseId",
                principalTable: "LifeCourse",
                principalColumn: "LifeCourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeCourse_Indicator_DefaultIndicatorId",
                table: "LifeCourse",
                column: "DefaultIndicatorId",
                principalTable: "Indicator",
                principalColumn: "IndicatorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Measure_Strata_DefaultStrataId",
                table: "Measure",
                column: "DefaultStrataId",
                principalTable: "Strata",
                principalColumn: "StrataId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_IndicatorGroup_DefaultIndicatorGroupId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_Measure_DefaultMeasureId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_IndicatorGroup_LifeCourse_DefaultLifeCourseId",
                table: "IndicatorGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_LifeCourse_Indicator_DefaultIndicatorId",
                table: "LifeCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_Measure_Strata_DefaultStrataId",
                table: "Measure");

            migrationBuilder.DropIndex(
                name: "IX_Measure_DefaultStrataId",
                table: "Measure");

            migrationBuilder.DropIndex(
                name: "IX_LifeCourse_DefaultIndicatorId",
                table: "LifeCourse");

            migrationBuilder.DropIndex(
                name: "IX_IndicatorGroup_DefaultLifeCourseId",
                table: "IndicatorGroup");

            migrationBuilder.DropIndex(
                name: "IX_Indicator_DefaultMeasureId",
                table: "Indicator");

            migrationBuilder.DropIndex(
                name: "IX_Activity_DefaultIndicatorGroupId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "DefaultStrataId",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "DefaultIndicatorId",
                table: "LifeCourse");

            migrationBuilder.DropColumn(
                name: "DefaultLifeCourseId",
                table: "IndicatorGroup");

            migrationBuilder.DropColumn(
                name: "DefaultMeasureId",
                table: "Indicator");

            migrationBuilder.DropColumn(
                name: "DefaultIndicatorGroupId",
                table: "Activity");
        }
    }
}

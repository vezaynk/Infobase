using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infobase.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IndicatorGroup",
                columns: table => new
                {
                    IndicatorGroupId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ActivityId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IndicatorGroupNameEn = table.Column<string>(nullable: true),
                    IndicatorGroupNameFr = table.Column<string>(nullable: true),
                    DefaultLifeCourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorGroup", x => x.IndicatorGroupId);
                    table.UniqueConstraint("AK_IndicatorGroup_Index", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    ActivityNameEn = table.Column<string>(nullable: true),
                    ActivityNameFr = table.Column<string>(nullable: true),
                    DefaultIndicatorGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityId);
                    table.UniqueConstraint("AK_Activity_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Activity_IndicatorGroup_DefaultIndicatorGroupId",
                        column: x => x.DefaultIndicatorGroupId,
                        principalTable: "IndicatorGroup",
                        principalColumn: "IndicatorGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LifeCourse",
                columns: table => new
                {
                    LifeCourseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IndicatorGroupId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    LifeCourseNameEn = table.Column<string>(nullable: true),
                    LifeCourseNameFr = table.Column<string>(nullable: true),
                    DefaultIndicatorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCourse", x => x.LifeCourseId);
                    table.UniqueConstraint("AK_LifeCourse_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_LifeCourse_IndicatorGroup_IndicatorGroupId",
                        column: x => x.IndicatorGroupId,
                        principalTable: "IndicatorGroup",
                        principalColumn: "IndicatorGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    MeasureId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IndicatorId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Included = table.Column<bool>(nullable: false),
                    Aggregator = table.Column<bool>(nullable: false),
                    CVWarnAt = table.Column<double>(nullable: true),
                    CVSuppressAt = table.Column<double>(nullable: true),
                    MeasureNameIndexEn = table.Column<string>(nullable: true),
                    MeasureNameDataToolEn = table.Column<string>(nullable: true),
                    MeasureDefinitionEn = table.Column<string>(nullable: true),
                    MeasureMethodEn = table.Column<string>(nullable: true),
                    MeasureAdditionalRemarksEn = table.Column<string>(nullable: true),
                    MeasureDataAvailableEn = table.Column<string>(nullable: true),
                    MeasurePopulationGroupEn = table.Column<string>(nullable: true),
                    MeasureSourceShortEn = table.Column<string>(nullable: true),
                    MeasureSourceLongEn = table.Column<string>(nullable: true),
                    MeasureUnitShortEn = table.Column<string>(nullable: true),
                    MeasureUnitLongEn = table.Column<string>(nullable: true),
                    MeasureNameIndexFr = table.Column<string>(nullable: true),
                    MeasureNameDataToolFr = table.Column<string>(nullable: true),
                    MeasureDefinitionFr = table.Column<string>(nullable: true),
                    MeasureMethodFr = table.Column<string>(nullable: true),
                    MeasureAdditionalRemarksFr = table.Column<string>(nullable: true),
                    MeasureDataAvailableFr = table.Column<string>(nullable: true),
                    MeasurePopulationGroupFr = table.Column<string>(nullable: true),
                    MeasureSourceShortFr = table.Column<string>(nullable: true),
                    MeasureSourceLongFr = table.Column<string>(nullable: true),
                    MeasureUnitShortFr = table.Column<string>(nullable: true),
                    MeasureUnitLongFr = table.Column<string>(nullable: true),
                    DefaultStrataId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.MeasureId);
                    table.UniqueConstraint("AK_Measure_Index", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Indicator",
                columns: table => new
                {
                    IndicatorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LifeCourseId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IndicatorNameEn = table.Column<string>(nullable: true),
                    IndicatorNameFr = table.Column<string>(nullable: true),
                    DefaultMeasureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicator", x => x.IndicatorId);
                    table.UniqueConstraint("AK_Indicator_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Indicator_Measure_DefaultMeasureId",
                        column: x => x.DefaultMeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Indicator_LifeCourse_LifeCourseId",
                        column: x => x.LifeCourseId,
                        principalTable: "LifeCourse",
                        principalColumn: "LifeCourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strata",
                columns: table => new
                {
                    StrataId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MeasureId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    StrataNotesEn = table.Column<string>(nullable: true),
                    StrataNameEn = table.Column<string>(nullable: true),
                    StrataSourceEn = table.Column<string>(nullable: true),
                    StrataPopulationTitleFragmentEn = table.Column<string>(nullable: true),
                    StrataNotesFr = table.Column<string>(nullable: true),
                    StrataNameFr = table.Column<string>(nullable: true),
                    StrataSourceFr = table.Column<string>(nullable: true),
                    StrataPopulationTitleFragmentFr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strata", x => x.StrataId);
                    table.UniqueConstraint("AK_Strata_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Strata_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    PointId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StrataId = table.Column<int>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    ValueAverage = table.Column<double>(nullable: true),
                    ValueUpper = table.Column<double>(nullable: true),
                    ValueLower = table.Column<double>(nullable: true),
                    CVInterpretation = table.Column<int>(nullable: false),
                    CVValue = table.Column<int>(nullable: true),
                    PointLabelEn = table.Column<string>(nullable: true),
                    PointLabelFr = table.Column<string>(nullable: true),
                    PointTextEn = table.Column<string>(nullable: true),
                    PointTextFr = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.PointId);
                    table.UniqueConstraint("AK_Point_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Point_Strata_StrataId",
                        column: x => x.StrataId,
                        principalTable: "Strata",
                        principalColumn: "StrataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_DefaultIndicatorGroupId",
                table: "Activity",
                column: "DefaultIndicatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicator_DefaultMeasureId",
                table: "Indicator",
                column: "DefaultMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicator_LifeCourseId",
                table: "Indicator",
                column: "LifeCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorGroup_ActivityId",
                table: "IndicatorGroup",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorGroup_DefaultLifeCourseId",
                table: "IndicatorGroup",
                column: "DefaultLifeCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCourse_DefaultIndicatorId",
                table: "LifeCourse",
                column: "DefaultIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCourse_IndicatorGroupId",
                table: "LifeCourse",
                column: "IndicatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Measure_DefaultStrataId",
                table: "Measure",
                column: "DefaultStrataId");

            migrationBuilder.CreateIndex(
                name: "IX_Measure_IndicatorId",
                table: "Measure",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_StrataId",
                table: "Point",
                column: "StrataId");

            migrationBuilder.CreateIndex(
                name: "IX_Strata_MeasureId",
                table: "Strata",
                column: "MeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndicatorGroup_LifeCourse_DefaultLifeCourseId",
                table: "IndicatorGroup",
                column: "DefaultLifeCourseId",
                principalTable: "LifeCourse",
                principalColumn: "LifeCourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IndicatorGroup_Activity_ActivityId",
                table: "IndicatorGroup",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeCourse_Indicator_DefaultIndicatorId",
                table: "LifeCourse",
                column: "DefaultIndicatorId",
                principalTable: "Indicator",
                principalColumn: "IndicatorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Measure_Indicator_IndicatorId",
                table: "Measure",
                column: "IndicatorId",
                principalTable: "Indicator",
                principalColumn: "IndicatorId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_LifeCourse_IndicatorGroup_IndicatorGroupId",
                table: "LifeCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_Measure_DefaultMeasureId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_Strata_Measure_MeasureId",
                table: "Strata");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_LifeCourse_LifeCourseId",
                table: "Indicator");

            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "IndicatorGroup");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Measure");

            migrationBuilder.DropTable(
                name: "Strata");

            migrationBuilder.DropTable(
                name: "LifeCourse");

            migrationBuilder.DropTable(
                name: "Indicator");
        }
    }
}

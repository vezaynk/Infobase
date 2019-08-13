using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Models.Migrations
{
    public partial class PASS2swtld01xlya : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Master",
                columns: table => new
                {
                    Index = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColActivity = table.Column<string>(nullable: true),
                    ColIndicatorGroup = table.Column<string>(nullable: true),
                    ColLifeCourse = table.Column<string>(nullable: true),
                    ColIndicator = table.Column<string>(nullable: true),
                    ColSpecificMeasure = table.Column<string>(nullable: true),
                    ColDataBreakdowns = table.Column<string>(nullable: true),
                    ColStrata = table.Column<string>(nullable: true),
                    ColCV = table.Column<string>(nullable: true),
                    ColData = table.Column<string>(nullable: true),
                    ColCILow95 = table.Column<string>(nullable: true),
                    ColCIUpper95 = table.Column<string>(nullable: true),
                    ColCVInterpretation = table.Column<string>(nullable: true),
                    ColCVRangeLower = table.Column<string>(nullable: true),
                    ColCVRangeUpper = table.Column<string>(nullable: true),
                    ColFeatureData = table.Column<string>(nullable: true),
                    ColPopulation1 = table.Column<string>(nullable: true),
                    ColUnitLabelLong = table.Column<string>(nullable: true),
                    ColDataSource1 = table.Column<string>(nullable: true),
                    ColNotes = table.Column<string>(nullable: true),
                    ColPTTableLabel = table.Column<string>(nullable: true),
                    ColUnitLabel2 = table.Column<string>(nullable: true),
                    ColDataSource2 = table.Column<string>(nullable: true),
                    ColSpecificMeasure2 = table.Column<string>(nullable: true),
                    ColDefintion = table.Column<string>(nullable: true),
                    ColDataSource3 = table.Column<string>(nullable: true),
                    ColDataAvailable = table.Column<string>(nullable: true),
                    ColPopulation2 = table.Column<string>(nullable: true),
                    ColEstimateCalculation = table.Column<string>(nullable: true),
                    ColAdditionalRemarks = table.Column<string>(nullable: true),
                    ColIsIncluded = table.Column<string>(nullable: true),
                    ColIsAggregator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorGroup",
                columns: table => new
                {
                    ColIndicatorGroupId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColLifeCourseId = table.Column<int>(nullable: true),
                    ColIndicatorGroupNameEn = table.Column<string>(nullable: true),
                    ColIndicatorGroupNameFr = table.Column<string>(nullable: true),
                    ColActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorGroup", x => x.ColIndicatorGroupId);
                    table.UniqueConstraint("AK_IndicatorGroup_Index", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ColActivityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColIndicatorGroupId = table.Column<int>(nullable: true),
                    ColActivityNameEn = table.Column<string>(nullable: true),
                    ColActivityNameFr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ColActivityId);
                    table.UniqueConstraint("AK_Activity_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Activity_IndicatorGroup_DefaultColIndicatorGroupId",
                        column: x => x.DefaultColIndicatorGroupId,
                        principalTable: "IndicatorGroup",
                        principalColumn: "ColIndicatorGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LifeCourse",
                columns: table => new
                {
                    ColLifeCourseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColIndicatorId = table.Column<int>(nullable: true),
                    ColLifeCourseNameEn = table.Column<string>(nullable: true),
                    ColLifeCourseNameFr = table.Column<string>(nullable: true),
                    ColIndicatorGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCourse", x => x.ColLifeCourseId);
                    table.UniqueConstraint("AK_LifeCourse_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_LifeCourse_IndicatorGroup_ColIndicatorGroupId",
                        column: x => x.ColIndicatorGroupId,
                        principalTable: "IndicatorGroup",
                        principalColumn: "ColIndicatorGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    ColSpecificMeasureId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColDataBreakdownsId = table.Column<int>(nullable: true),
                    ColSpecificMeasureNameEn = table.Column<string>(nullable: true),
                    ColSpecificMeasureNameFr = table.Column<string>(nullable: true),
                    ColIndicatorId = table.Column<int>(nullable: false),
                    Include = table.Column<bool>(nullable: false),
                    IsAggregator = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.ColSpecificMeasureId);
                    table.UniqueConstraint("AK_Measure_Index", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Indicator",
                columns: table => new
                {
                    ColIndicatorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColSpecificMeasureId = table.Column<int>(nullable: true),
                    ColIndicatorNameEn = table.Column<string>(nullable: true),
                    ColIndicatorNameFr = table.Column<string>(nullable: true),
                    ColLifeCourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicator", x => x.ColIndicatorId);
                    table.UniqueConstraint("AK_Indicator_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Indicator_LifeCourse_ColLifeCourseId",
                        column: x => x.ColLifeCourseId,
                        principalTable: "LifeCourse",
                        principalColumn: "ColLifeCourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Indicator_Measure_DefaultColSpecificMeasureId",
                        column: x => x.DefaultColSpecificMeasureId,
                        principalTable: "Measure",
                        principalColumn: "ColSpecificMeasureId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    ColDataBreakdownsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColStrataId = table.Column<int>(nullable: true),
                    ColDataBreakdownsNameEn = table.Column<string>(nullable: true),
                    ColDataBreakdownsNameFr = table.Column<string>(nullable: true),
                    ColSpecificMeasureId = table.Column<int>(nullable: false),
                    CVRangeLower = table.Column<double>(nullable: true),
                    CVRangeUpper = table.Column<double>(nullable: true),
                    UnitLong = table.Column<string>(nullable: true),
                    UnitShort = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.ColDataBreakdownsId);
                    table.UniqueConstraint("AK_Point_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Point_Measure_ColSpecificMeasureId",
                        column: x => x.ColSpecificMeasureId,
                        principalTable: "Measure",
                        principalColumn: "ColSpecificMeasureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strata",
                columns: table => new
                {
                    ColStrataId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    ColStrataNameEn = table.Column<string>(nullable: true),
                    ColStrataNameFr = table.Column<string>(nullable: true),
                    ColDataBreakdownsId = table.Column<int>(nullable: false),
                    ValueAverage = table.Column<double>(nullable: true),
                    ValueUpper = table.Column<double>(nullable: true),
                    ValueLower = table.Column<double>(nullable: true),
                    CVInterpretation = table.Column<int>(nullable: false),
                    CVValue = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strata", x => x.ColStrataId);
                    table.UniqueConstraint("AK_Strata_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Strata_Point_ColDataBreakdownsId",
                        column: x => x.ColDataBreakdownsId,
                        principalTable: "Point",
                        principalColumn: "ColDataBreakdownsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_DefaultColIndicatorGroupId",
                table: "Activity",
                column: "DefaultColIndicatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicator_ColLifeCourseId",
                table: "Indicator",
                column: "ColLifeCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicator_DefaultColSpecificMeasureId",
                table: "Indicator",
                column: "DefaultColSpecificMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorGroup_ColActivityId",
                table: "IndicatorGroup",
                column: "ColActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorGroup_DefaultColLifeCourseId",
                table: "IndicatorGroup",
                column: "DefaultColLifeCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCourse_ColIndicatorGroupId",
                table: "LifeCourse",
                column: "ColIndicatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCourse_DefaultColIndicatorId",
                table: "LifeCourse",
                column: "DefaultColIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Measure_ColIndicatorId",
                table: "Measure",
                column: "ColIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Measure_DefaultColDataBreakdownsId",
                table: "Measure",
                column: "DefaultColDataBreakdownsId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_ColSpecificMeasureId",
                table: "Point",
                column: "ColSpecificMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_DefaultColStrataId",
                table: "Point",
                column: "DefaultColStrataId");

            migrationBuilder.CreateIndex(
                name: "IX_Strata_ColDataBreakdownsId",
                table: "Strata",
                column: "ColDataBreakdownsId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndicatorGroup_LifeCourse_DefaultColLifeCourseId",
                table: "IndicatorGroup",
                column: "DefaultColLifeCourseId",
                principalTable: "LifeCourse",
                principalColumn: "ColLifeCourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IndicatorGroup_Activity_ColActivityId",
                table: "IndicatorGroup",
                column: "ColActivityId",
                principalTable: "Activity",
                principalColumn: "ColActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LifeCourse_Indicator_DefaultColIndicatorId",
                table: "LifeCourse",
                column: "DefaultColIndicatorId",
                principalTable: "Indicator",
                principalColumn: "ColIndicatorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Measure_Indicator_ColIndicatorId",
                table: "Measure",
                column: "ColIndicatorId",
                principalTable: "Indicator",
                principalColumn: "ColIndicatorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Measure_Point_DefaultColDataBreakdownsId",
                table: "Measure",
                column: "DefaultColDataBreakdownsId",
                principalTable: "Point",
                principalColumn: "ColDataBreakdownsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Strata_DefaultColStrataId",
                table: "Point",
                column: "DefaultColStrataId",
                principalTable: "Strata",
                principalColumn: "ColStrataId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_IndicatorGroup_DefaultColIndicatorGroupId",
                table: "Activity");

            migrationBuilder.DropForeignKey(
                name: "FK_LifeCourse_IndicatorGroup_ColIndicatorGroupId",
                table: "LifeCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_LifeCourse_ColLifeCourseId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_Measure_DefaultColSpecificMeasureId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_Point_Measure_ColSpecificMeasureId",
                table: "Point");

            migrationBuilder.DropForeignKey(
                name: "FK_Strata_Point_ColDataBreakdownsId",
                table: "Strata");

            migrationBuilder.DropTable(
                name: "Master");

            migrationBuilder.DropTable(
                name: "IndicatorGroup");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "LifeCourse");

            migrationBuilder.DropTable(
                name: "Measure");

            migrationBuilder.DropTable(
                name: "Indicator");

            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "Strata");
        }
    }
}

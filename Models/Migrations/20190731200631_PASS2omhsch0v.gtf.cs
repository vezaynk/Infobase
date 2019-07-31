using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Models.Migrations
{
    public partial class PASS2omhsch0vgtf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Master",
                columns: table => new
                {
                    Index = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Activity = table.Column<string>(nullable: true),
                    IndicatorGroup = table.Column<string>(nullable: true),
                    LifeCourse = table.Column<string>(nullable: true),
                    Indicator = table.Column<string>(nullable: true),
                    SpecificMeasure = table.Column<string>(nullable: true),
                    DataBreakdowns = table.Column<string>(nullable: true),
                    Strata = table.Column<string>(nullable: true),
                    CV = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    CILow95 = table.Column<string>(nullable: true),
                    CIUpper95 = table.Column<string>(nullable: true),
                    CVInterpretation = table.Column<string>(nullable: true),
                    CVRangeLower = table.Column<string>(nullable: true),
                    CVRangeUpper = table.Column<string>(nullable: true),
                    FeatureData = table.Column<string>(nullable: true),
                    Population1 = table.Column<string>(nullable: true),
                    UnitLabelLong = table.Column<string>(nullable: true),
                    DataSource1 = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    PTTableLabel = table.Column<string>(nullable: true),
                    UnitLabel2 = table.Column<string>(nullable: true),
                    DataSource2 = table.Column<string>(nullable: true),
                    SpecificMeasure2 = table.Column<string>(nullable: true),
                    Defintion = table.Column<string>(nullable: true),
                    DataSource3 = table.Column<string>(nullable: true),
                    DataAvailable = table.Column<string>(nullable: true),
                    Population2 = table.Column<string>(nullable: true),
                    EstimateCalculation = table.Column<string>(nullable: true),
                    AdditionalRemarks = table.Column<string>(nullable: true),
                    IsIncluded = table.Column<string>(nullable: true),
                    IsAggregator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorGroup",
                columns: table => new
                {
                    IndicatorGroupId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultLifeCourseId = table.Column<int>(nullable: true),
                    IndicatorGroupNameEn = table.Column<string>(nullable: true),
                    IndicatorGroupNameFr = table.Column<string>(nullable: true),
                    ActivityId = table.Column<int>(nullable: false)
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
                    DefaultIndicatorGroupId = table.Column<int>(nullable: true),
                    ActivityNameEn = table.Column<string>(nullable: true),
                    ActivityNameFr = table.Column<string>(nullable: true)
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
                    Index = table.Column<int>(nullable: false),
                    DefaultIndicatorId = table.Column<int>(nullable: true),
                    LifeCourseNameEn = table.Column<string>(nullable: true),
                    LifeCourseNameFr = table.Column<string>(nullable: true),
                    IndicatorGroupId = table.Column<int>(nullable: false)
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
                    SpecificMeasureId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultDataBreakdownsId = table.Column<int>(nullable: true),
                    SpecificMeasureNameEn = table.Column<string>(nullable: true),
                    SpecificMeasureNameFr = table.Column<string>(nullable: true),
                    IndicatorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.SpecificMeasureId);
                    table.UniqueConstraint("AK_Measure_Index", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Indicator",
                columns: table => new
                {
                    IndicatorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultSpecificMeasureId = table.Column<int>(nullable: true),
                    IndicatorNameEn = table.Column<string>(nullable: true),
                    IndicatorNameFr = table.Column<string>(nullable: true),
                    LifeCourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicator", x => x.IndicatorId);
                    table.UniqueConstraint("AK_Indicator_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Indicator_Measure_DefaultSpecificMeasureId",
                        column: x => x.DefaultSpecificMeasureId,
                        principalTable: "Measure",
                        principalColumn: "SpecificMeasureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Indicator_LifeCourse_LifeCourseId",
                        column: x => x.LifeCourseId,
                        principalTable: "LifeCourse",
                        principalColumn: "LifeCourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Point",
                columns: table => new
                {
                    DataBreakdownsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultStrataId = table.Column<int>(nullable: true),
                    DataBreakdownsNameEn = table.Column<string>(nullable: true),
                    DataBreakdownsNameFr = table.Column<string>(nullable: true),
                    SpecificMeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.DataBreakdownsId);
                    table.UniqueConstraint("AK_Point_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Point_Measure_SpecificMeasureId",
                        column: x => x.SpecificMeasureId,
                        principalTable: "Measure",
                        principalColumn: "SpecificMeasureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strata",
                columns: table => new
                {
                    StrataId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    StrataNameEn = table.Column<string>(nullable: true),
                    StrataNameFr = table.Column<string>(nullable: true),
                    DataBreakdownsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strata", x => x.StrataId);
                    table.UniqueConstraint("AK_Strata_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_Strata_Point_DataBreakdownsId",
                        column: x => x.DataBreakdownsId,
                        principalTable: "Point",
                        principalColumn: "DataBreakdownsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_DefaultIndicatorGroupId",
                table: "Activity",
                column: "DefaultIndicatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicator_DefaultSpecificMeasureId",
                table: "Indicator",
                column: "DefaultSpecificMeasureId");

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
                name: "IX_Measure_DefaultDataBreakdownsId",
                table: "Measure",
                column: "DefaultDataBreakdownsId");

            migrationBuilder.CreateIndex(
                name: "IX_Measure_IndicatorId",
                table: "Measure",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_DefaultStrataId",
                table: "Point",
                column: "DefaultStrataId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_SpecificMeasureId",
                table: "Point",
                column: "SpecificMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Strata_DataBreakdownsId",
                table: "Strata",
                column: "DataBreakdownsId");

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
                name: "FK_Measure_Point_DefaultDataBreakdownsId",
                table: "Measure",
                column: "DefaultDataBreakdownsId",
                principalTable: "Point",
                principalColumn: "DataBreakdownsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Point_Strata_DefaultStrataId",
                table: "Point",
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
                name: "FK_Indicator_Measure_DefaultSpecificMeasureId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_Point_Measure_SpecificMeasureId",
                table: "Point");

            migrationBuilder.DropForeignKey(
                name: "FK_Indicator_LifeCourse_LifeCourseId",
                table: "Indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_Strata_Point_DataBreakdownsId",
                table: "Strata");

            migrationBuilder.DropTable(
                name: "Master");

            migrationBuilder.DropTable(
                name: "IndicatorGroup");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Measure");

            migrationBuilder.DropTable(
                name: "LifeCourse");

            migrationBuilder.DropTable(
                name: "Indicator");

            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "Strata");
        }
    }
}

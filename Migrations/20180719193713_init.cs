using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReactDotNetDemo.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageCode = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.TranslationId);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorGroup",
                columns: table => new
                {
                    IndicatorGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorGroup", x => x.IndicatorGroupId);
                    table.ForeignKey(
                        name: "FK_IndicatorGroup_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityDescriptionTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    ActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDescriptionTranslation", x => new { x.TranslationId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_ActivityDescriptionTranslation_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityDescriptionTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityNameTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    ActivityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityNameTranslation", x => new { x.TranslationId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_ActivityNameTranslation_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityNameTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorGroupNameTranslation",
                columns: table => new
                {
                    IndicatorGroupId = table.Column<int>(nullable: false),
                    TranslationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorGroupNameTranslation", x => new { x.TranslationId, x.IndicatorGroupId });
                    table.ForeignKey(
                        name: "FK_IndicatorGroupNameTranslation_IndicatorGroup_IndicatorGroupId",
                        column: x => x.IndicatorGroupId,
                        principalTable: "IndicatorGroup",
                        principalColumn: "IndicatorGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicatorGroupNameTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeCourse",
                columns: table => new
                {
                    LifeCourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndicatorGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCourse", x => x.LifeCourseId);
                    table.ForeignKey(
                        name: "FK_LifeCourse_IndicatorGroup_IndicatorGroupId",
                        column: x => x.IndicatorGroupId,
                        principalTable: "IndicatorGroup",
                        principalColumn: "IndicatorGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Indicator",
                columns: table => new
                {
                    IndicatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LifeCourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicator", x => x.IndicatorId);
                    table.ForeignKey(
                        name: "FK_Indicator_LifeCourse_LifeCourseId",
                        column: x => x.LifeCourseId,
                        principalTable: "LifeCourse",
                        principalColumn: "LifeCourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LifeCourseNameTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    LifeCourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeCourseNameTranslation", x => new { x.TranslationId, x.LifeCourseId });
                    table.ForeignKey(
                        name: "FK_LifeCourseNameTranslation_LifeCourse_LifeCourseId",
                        column: x => x.LifeCourseId,
                        principalTable: "LifeCourse",
                        principalColumn: "LifeCourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LifeCourseNameTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorNameTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    IndicatorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorNameTranslation", x => new { x.TranslationId, x.IndicatorId });
                    table.ForeignKey(
                        name: "FK_IndicatorNameTranslation_Indicator_IndicatorId",
                        column: x => x.IndicatorId,
                        principalTable: "Indicator",
                        principalColumn: "IndicatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndicatorNameTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measure",
                columns: table => new
                {
                    MeasureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndicatorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measure", x => x.MeasureId);
                    table.ForeignKey(
                        name: "FK_Measure_Indicator_IndicatorId",
                        column: x => x.IndicatorId,
                        principalTable: "Indicator",
                        principalColumn: "IndicatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasureDefinitionTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureDefinitionTranslation", x => new { x.TranslationId, x.MeasureId });
                    table.ForeignKey(
                        name: "FK_MeasureDefinitionTranslation_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasureDefinitionTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasureNameTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureNameTranslation", x => new { x.TranslationId, x.MeasureId });
                    table.ForeignKey(
                        name: "FK_MeasureNameTranslation_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasureNameTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasureUnitTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnitTranslation", x => new { x.TranslationId, x.MeasureId });
                    table.ForeignKey(
                        name: "FK_MeasureUnitTranslation_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasureUnitTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strata",
                columns: table => new
                {
                    StrataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strata", x => x.StrataId);
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
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StrataId = table.Column<int>(nullable: false),
                    ValueAverage = table.Column<double>(nullable: true),
                    ValueUpper = table.Column<double>(nullable: true),
                    ValueLower = table.Column<double>(nullable: true),
                    CVInterpretation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Point", x => x.PointId);
                    table.ForeignKey(
                        name: "FK_Point_Strata_StrataId",
                        column: x => x.StrataId,
                        principalTable: "Strata",
                        principalColumn: "StrataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrataNameTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    StrataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrataNameTranslation", x => new { x.TranslationId, x.StrataId });
                    table.ForeignKey(
                        name: "FK_StrataNameTranslation_Strata_StrataId",
                        column: x => x.StrataId,
                        principalTable: "Strata",
                        principalColumn: "StrataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StrataNameTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrataNotesTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    StrataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrataNotesTranslation", x => new { x.TranslationId, x.StrataId });
                    table.ForeignKey(
                        name: "FK_StrataNotesTranslation_Strata_StrataId",
                        column: x => x.StrataId,
                        principalTable: "Strata",
                        principalColumn: "StrataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StrataNotesTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrataPopulationTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    StrataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrataPopulationTranslation", x => new { x.TranslationId, x.StrataId });
                    table.ForeignKey(
                        name: "FK_StrataPopulationTranslation_Strata_StrataId",
                        column: x => x.StrataId,
                        principalTable: "Strata",
                        principalColumn: "StrataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StrataPopulationTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StrataSourceTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    StrataId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrataSourceTranslation", x => new { x.TranslationId, x.StrataId });
                    table.ForeignKey(
                        name: "FK_StrataSourceTranslation_Strata_StrataId",
                        column: x => x.StrataId,
                        principalTable: "Strata",
                        principalColumn: "StrataId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StrataSourceTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointLabelTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    PointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointLabelTranslation", x => new { x.TranslationId, x.PointId });
                    table.ForeignKey(
                        name: "FK_PointLabelTranslation_Point_PointId",
                        column: x => x.PointId,
                        principalTable: "Point",
                        principalColumn: "PointId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointLabelTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDescriptionTranslation_ActivityId",
                table: "ActivityDescriptionTranslation",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityNameTranslation_ActivityId",
                table: "ActivityNameTranslation",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Indicator_LifeCourseId",
                table: "Indicator",
                column: "LifeCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorGroup_ActivityId",
                table: "IndicatorGroup",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorGroupNameTranslation_IndicatorGroupId",
                table: "IndicatorGroupNameTranslation",
                column: "IndicatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorNameTranslation_IndicatorId",
                table: "IndicatorNameTranslation",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCourse_IndicatorGroupId",
                table: "LifeCourse",
                column: "IndicatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeCourseNameTranslation_LifeCourseId",
                table: "LifeCourseNameTranslation",
                column: "LifeCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Measure_IndicatorId",
                table: "Measure",
                column: "IndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureDefinitionTranslation_MeasureId",
                table: "MeasureDefinitionTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureNameTranslation_MeasureId",
                table: "MeasureNameTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureUnitTranslation_MeasureId",
                table: "MeasureUnitTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Point_StrataId",
                table: "Point",
                column: "StrataId");

            migrationBuilder.CreateIndex(
                name: "IX_PointLabelTranslation_PointId",
                table: "PointLabelTranslation",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_Strata_MeasureId",
                table: "Strata",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_StrataNameTranslation_StrataId",
                table: "StrataNameTranslation",
                column: "StrataId");

            migrationBuilder.CreateIndex(
                name: "IX_StrataNotesTranslation_StrataId",
                table: "StrataNotesTranslation",
                column: "StrataId");

            migrationBuilder.CreateIndex(
                name: "IX_StrataPopulationTranslation_StrataId",
                table: "StrataPopulationTranslation",
                column: "StrataId");

            migrationBuilder.CreateIndex(
                name: "IX_StrataSourceTranslation_StrataId",
                table: "StrataSourceTranslation",
                column: "StrataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityDescriptionTranslation");

            migrationBuilder.DropTable(
                name: "ActivityNameTranslation");

            migrationBuilder.DropTable(
                name: "IndicatorGroupNameTranslation");

            migrationBuilder.DropTable(
                name: "IndicatorNameTranslation");

            migrationBuilder.DropTable(
                name: "LifeCourseNameTranslation");

            migrationBuilder.DropTable(
                name: "MeasureDefinitionTranslation");

            migrationBuilder.DropTable(
                name: "MeasureNameTranslation");

            migrationBuilder.DropTable(
                name: "MeasureUnitTranslation");

            migrationBuilder.DropTable(
                name: "PointLabelTranslation");

            migrationBuilder.DropTable(
                name: "StrataNameTranslation");

            migrationBuilder.DropTable(
                name: "StrataNotesTranslation");

            migrationBuilder.DropTable(
                name: "StrataPopulationTranslation");

            migrationBuilder.DropTable(
                name: "StrataSourceTranslation");

            migrationBuilder.DropTable(
                name: "Point");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "Strata");

            migrationBuilder.DropTable(
                name: "Measure");

            migrationBuilder.DropTable(
                name: "Indicator");

            migrationBuilder.DropTable(
                name: "LifeCourse");

            migrationBuilder.DropTable(
                name: "IndicatorGroup");

            migrationBuilder.DropTable(
                name: "Activity");
        }
    }
}

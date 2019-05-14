using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infobase.Migrations
{
    public partial class denormalize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "MeasureAdditionalRemarksTranslation");

            migrationBuilder.DropTable(
                name: "MeasureDataAvailableTranslation");

            migrationBuilder.DropTable(
                name: "MeasureDefinitionTranslation");

            migrationBuilder.DropTable(
                name: "MeasureMethodTranslation");

            migrationBuilder.DropTable(
                name: "MeasureNameTranslation");

            migrationBuilder.DropTable(
                name: "MeasurePopulationTranslation");

            migrationBuilder.DropTable(
                name: "MeasureSourceTranslation");

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
                name: "Translation");

            migrationBuilder.AddColumn<string>(
                name: "StrataNameEn",
                table: "Strata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrataNameFr",
                table: "Strata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrataNotesEn",
                table: "Strata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrataNotesFr",
                table: "Strata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrataPopulationEn",
                table: "Strata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrataPopulationFr",
                table: "Strata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrataSourceEn",
                table: "Strata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StrataSourceFr",
                table: "Strata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PointLabelEn",
                table: "Point",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PointLabelFr",
                table: "Point",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureAdditionalRemarksEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureAdditionalRemarksFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureDataAvailableEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureDataAvailableFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureDefinitionEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureDefinitionFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureMethodEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureMethodFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureNameEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureNameFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasurePopulationEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasurePopulationFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureSourceEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureSourceFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureUnitEn",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MeasureUnitFr",
                table: "Measure",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LifeCourseNameEn",
                table: "LifeCourse",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LifeCourseNameFr",
                table: "LifeCourse",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndicatorGroupNameEn",
                table: "IndicatorGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndicatorGroupNameFr",
                table: "IndicatorGroup",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndicatorNameEn",
                table: "Indicator",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndicatorNameFr",
                table: "Indicator",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivityNameEn",
                table: "Activity",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActivityNameFr",
                table: "Activity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StrataNameEn",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "StrataNameFr",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "StrataNotesEn",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "StrataNotesFr",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "StrataPopulationEn",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "StrataPopulationFr",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "StrataSourceEn",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "StrataSourceFr",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "PointLabelEn",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "PointLabelFr",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "MeasureAdditionalRemarksEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureAdditionalRemarksFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureDataAvailableEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureDataAvailableFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureDefinitionEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureDefinitionFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureMethodEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureMethodFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureNameEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureNameFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasurePopulationEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasurePopulationFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureSourceEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureSourceFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureUnitEn",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "MeasureUnitFr",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "LifeCourseNameEn",
                table: "LifeCourse");

            migrationBuilder.DropColumn(
                name: "LifeCourseNameFr",
                table: "LifeCourse");

            migrationBuilder.DropColumn(
                name: "IndicatorGroupNameEn",
                table: "IndicatorGroup");

            migrationBuilder.DropColumn(
                name: "IndicatorGroupNameFr",
                table: "IndicatorGroup");

            migrationBuilder.DropColumn(
                name: "IndicatorNameEn",
                table: "Indicator");

            migrationBuilder.DropColumn(
                name: "IndicatorNameFr",
                table: "Indicator");

            migrationBuilder.DropColumn(
                name: "ActivityNameEn",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "ActivityNameFr",
                table: "Activity");

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageCode = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.TranslationId);
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
                    TranslationId = table.Column<int>(nullable: false),
                    IndicatorGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorGroupNameTranslation", x => new { x.TranslationId, x.IndicatorGroupId });
                    table.ForeignKey(
                        name: "FK_IndicatorGroupNameTranslation_IndicatorGroup_IndicatorGroup~",
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
                name: "MeasureAdditionalRemarksTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureAdditionalRemarksTranslation", x => new { x.TranslationId, x.MeasureId });
                    table.ForeignKey(
                        name: "FK_MeasureAdditionalRemarksTranslation_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasureAdditionalRemarksTranslation_Translation_Translation~",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasureDataAvailableTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureDataAvailableTranslation", x => new { x.TranslationId, x.MeasureId });
                    table.ForeignKey(
                        name: "FK_MeasureDataAvailableTranslation_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasureDataAvailableTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
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
                name: "MeasureMethodTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureMethodTranslation", x => new { x.TranslationId, x.MeasureId });
                    table.ForeignKey(
                        name: "FK_MeasureMethodTranslation_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasureMethodTranslation_Translation_TranslationId",
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
                name: "MeasurePopulationTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurePopulationTranslation", x => new { x.TranslationId, x.MeasureId });
                    table.ForeignKey(
                        name: "FK_MeasurePopulationTranslation_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurePopulationTranslation_Translation_TranslationId",
                        column: x => x.TranslationId,
                        principalTable: "Translation",
                        principalColumn: "TranslationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasureSourceTranslation",
                columns: table => new
                {
                    TranslationId = table.Column<int>(nullable: false),
                    MeasureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureSourceTranslation", x => new { x.TranslationId, x.MeasureId });
                    table.ForeignKey(
                        name: "FK_MeasureSourceTranslation_Measure_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measure",
                        principalColumn: "MeasureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasureSourceTranslation_Translation_TranslationId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDescriptionTranslation_ActivityId",
                table: "ActivityDescriptionTranslation",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityNameTranslation_ActivityId",
                table: "ActivityNameTranslation",
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
                name: "IX_LifeCourseNameTranslation_LifeCourseId",
                table: "LifeCourseNameTranslation",
                column: "LifeCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureAdditionalRemarksTranslation_MeasureId",
                table: "MeasureAdditionalRemarksTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureDataAvailableTranslation_MeasureId",
                table: "MeasureDataAvailableTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureDefinitionTranslation_MeasureId",
                table: "MeasureDefinitionTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureMethodTranslation_MeasureId",
                table: "MeasureMethodTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureNameTranslation_MeasureId",
                table: "MeasureNameTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurePopulationTranslation_MeasureId",
                table: "MeasurePopulationTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureSourceTranslation_MeasureId",
                table: "MeasureSourceTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureUnitTranslation_MeasureId",
                table: "MeasureUnitTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PointLabelTranslation_PointId",
                table: "PointLabelTranslation",
                column: "PointId");

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
    }
}

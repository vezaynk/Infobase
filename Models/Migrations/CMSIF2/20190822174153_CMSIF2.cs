using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Models.Migrations
{
    public partial class CMSIF2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Master",
                columns: table => new
                {
                    Index = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColObs = table.Column<string>(nullable: true),
                    ColDomain = table.Column<string>(nullable: true),
                    ColIndicator = table.Column<string>(nullable: true),
                    ColMeasures = table.Column<string>(nullable: true),
                    ColDataBreakdowns = table.Column<string>(nullable: true),
                    ColDisaggregation = table.Column<string>(nullable: true),
                    ColNObs = table.Column<string>(nullable: true),
                    ColPercent = table.Column<string>(nullable: true),
                    ColCILow95 = table.Column<string>(nullable: true),
                    ColCIUpper95 = table.Column<string>(nullable: true),
                    ColCV = table.Column<string>(nullable: true),
                    ColCVInterpretation = table.Column<string>(nullable: true),
                    ColLabel = table.Column<string>(nullable: true),
                    ColSource = table.Column<string>(nullable: true),
                    ColNotes = table.Column<string>(nullable: true),
                    ColPopulation = table.Column<string>(nullable: true),
                    ColNW = table.Column<string>(nullable: true),
                    ColMeasure = table.Column<string>(nullable: true),
                    ColDefinition = table.Column<string>(nullable: true),
                    ColDataSource = table.Column<string>(nullable: true),
                    ColDataAvailable = table.Column<string>(nullable: true),
                    ColEstimateCalculation = table.Column<string>(nullable: true),
                    ColAdditionalRemarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "ColDisaggregation",
                columns: table => new
                {
                    ColDisaggregationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    ColDisaggregationNameEn = table.Column<string>(nullable: true),
                    ColDisaggregationNameFr = table.Column<string>(nullable: true),
                    ColDataBreakdownsId = table.Column<int>(nullable: false),
                    ValueAverage = table.Column<double>(nullable: true),
                    ValueUpper = table.Column<double>(nullable: true),
                    ValueLower = table.Column<double>(nullable: true),
                    CVInterpretation = table.Column<int>(nullable: false),
                    CVValue = table.Column<double>(nullable: true),
                    DataLabelChartEn = table.Column<string>(nullable: true),
                    DataLabelChartFr = table.Column<string>(nullable: true),
                    DataLabelTableEn = table.Column<string>(nullable: true),
                    DataLabelTableFr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColDisaggregation", x => x.ColDisaggregationId);
                    table.UniqueConstraint("AK_ColDisaggregation_Index", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "ColMeasures",
                columns: table => new
                {
                    ColMeasuresId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColDataBreakdownsId = table.Column<int>(nullable: true),
                    ColMeasuresNameEn = table.Column<string>(nullable: true),
                    ColMeasuresNameFr = table.Column<string>(nullable: true),
                    ColIndicatorId = table.Column<int>(nullable: false),
                    ColPopulationEn = table.Column<string>(nullable: true),
                    ColPopulationFr = table.Column<string>(nullable: true),
                    ColMeasureEn = table.Column<string>(nullable: true),
                    ColMeasureFr = table.Column<string>(nullable: true),
                    ColDefinitionEn = table.Column<string>(nullable: true),
                    ColDefinitionFr = table.Column<string>(nullable: true),
                    ColDataSourceEn = table.Column<string>(nullable: true),
                    ColDataSourceFr = table.Column<string>(nullable: true),
                    ColDataAvailableEn = table.Column<string>(nullable: true),
                    ColDataAvailableFr = table.Column<string>(nullable: true),
                    ColEstimateCalculationEn = table.Column<string>(nullable: true),
                    ColEstimateCalculationFr = table.Column<string>(nullable: true),
                    Include = table.Column<bool>(nullable: false),
                    IsAggregator = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColMeasures", x => x.ColMeasuresId);
                    table.UniqueConstraint("AK_ColMeasures_Index", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "ColDataBreakdowns",
                columns: table => new
                {
                    ColDataBreakdownsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColDisaggregationId = table.Column<int>(nullable: true),
                    ColDataBreakdownsNameEn = table.Column<string>(nullable: true),
                    ColDataBreakdownsNameFr = table.Column<string>(nullable: true),
                    ColMeasuresId = table.Column<int>(nullable: false),
                    ColSourceEn = table.Column<string>(nullable: true),
                    ColSourceFr = table.Column<string>(nullable: true),
                    ColNotesEn = table.Column<string>(nullable: true),
                    ColNotesFr = table.Column<string>(nullable: true),
                    ColAdditionalRemarksEn = table.Column<string>(nullable: true),
                    ColAdditionalRemarksFr = table.Column<string>(nullable: true),
                    CVRangeLower = table.Column<double>(nullable: true),
                    CVRangeUpper = table.Column<double>(nullable: true),
                    UnitLongEn = table.Column<string>(nullable: true),
                    UnitLongFr = table.Column<string>(nullable: true),
                    UnitShortEn = table.Column<string>(nullable: true),
                    UnitShortFr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColDataBreakdowns", x => x.ColDataBreakdownsId);
                    table.UniqueConstraint("AK_ColDataBreakdowns_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_ColDataBreakdowns_ColMeasures_ColMeasuresId",
                        column: x => x.ColMeasuresId,
                        principalTable: "ColMeasures",
                        principalColumn: "ColMeasuresId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColDataBreakdowns_ColDisaggregation_DefaultColDisaggregatio~",
                        column: x => x.DefaultColDisaggregationId,
                        principalTable: "ColDisaggregation",
                        principalColumn: "ColDisaggregationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ColIndicator",
                columns: table => new
                {
                    ColIndicatorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColMeasuresId = table.Column<int>(nullable: true),
                    ColIndicatorNameEn = table.Column<string>(nullable: true),
                    ColIndicatorNameFr = table.Column<string>(nullable: true),
                    ColDomainId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColIndicator", x => x.ColIndicatorId);
                    table.UniqueConstraint("AK_ColIndicator_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_ColIndicator_ColMeasures_DefaultColMeasuresId",
                        column: x => x.DefaultColMeasuresId,
                        principalTable: "ColMeasures",
                        principalColumn: "ColMeasuresId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ColDomain",
                columns: table => new
                {
                    ColDomainId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Index = table.Column<int>(nullable: false),
                    DefaultColIndicatorId = table.Column<int>(nullable: true),
                    ColDomainNameEn = table.Column<string>(nullable: true),
                    ColDomainNameFr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColDomain", x => x.ColDomainId);
                    table.UniqueConstraint("AK_ColDomain_Index", x => x.Index);
                    table.ForeignKey(
                        name: "FK_ColDomain_ColIndicator_DefaultColIndicatorId",
                        column: x => x.DefaultColIndicatorId,
                        principalTable: "ColIndicator",
                        principalColumn: "ColIndicatorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColDataBreakdowns_ColMeasuresId",
                table: "ColDataBreakdowns",
                column: "ColMeasuresId");

            migrationBuilder.CreateIndex(
                name: "IX_ColDataBreakdowns_DefaultColDisaggregationId",
                table: "ColDataBreakdowns",
                column: "DefaultColDisaggregationId");

            migrationBuilder.CreateIndex(
                name: "IX_ColDisaggregation_ColDataBreakdownsId",
                table: "ColDisaggregation",
                column: "ColDataBreakdownsId");

            migrationBuilder.CreateIndex(
                name: "IX_ColDomain_DefaultColIndicatorId",
                table: "ColDomain",
                column: "DefaultColIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColIndicator_ColDomainId",
                table: "ColIndicator",
                column: "ColDomainId");

            migrationBuilder.CreateIndex(
                name: "IX_ColIndicator_DefaultColMeasuresId",
                table: "ColIndicator",
                column: "DefaultColMeasuresId");

            migrationBuilder.CreateIndex(
                name: "IX_ColMeasures_ColIndicatorId",
                table: "ColMeasures",
                column: "ColIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ColMeasures_DefaultColDataBreakdownsId",
                table: "ColMeasures",
                column: "DefaultColDataBreakdownsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColDisaggregation_ColDataBreakdowns_ColDataBreakdownsId",
                table: "ColDisaggregation",
                column: "ColDataBreakdownsId",
                principalTable: "ColDataBreakdowns",
                principalColumn: "ColDataBreakdownsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColMeasures_ColDataBreakdowns_DefaultColDataBreakdownsId",
                table: "ColMeasures",
                column: "DefaultColDataBreakdownsId",
                principalTable: "ColDataBreakdowns",
                principalColumn: "ColDataBreakdownsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ColMeasures_ColIndicator_ColIndicatorId",
                table: "ColMeasures",
                column: "ColIndicatorId",
                principalTable: "ColIndicator",
                principalColumn: "ColIndicatorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ColIndicator_ColDomain_ColDomainId",
                table: "ColIndicator",
                column: "ColDomainId",
                principalTable: "ColDomain",
                principalColumn: "ColDomainId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColDataBreakdowns_ColMeasures_ColMeasuresId",
                table: "ColDataBreakdowns");

            migrationBuilder.DropForeignKey(
                name: "FK_ColIndicator_ColMeasures_DefaultColMeasuresId",
                table: "ColIndicator");

            migrationBuilder.DropForeignKey(
                name: "FK_ColDataBreakdowns_ColDisaggregation_DefaultColDisaggregatio~",
                table: "ColDataBreakdowns");

            migrationBuilder.DropForeignKey(
                name: "FK_ColDomain_ColIndicator_DefaultColIndicatorId",
                table: "ColDomain");

            migrationBuilder.DropTable(
                name: "Master");

            migrationBuilder.DropTable(
                name: "ColMeasures");

            migrationBuilder.DropTable(
                name: "ColDisaggregation");

            migrationBuilder.DropTable(
                name: "ColDataBreakdowns");

            migrationBuilder.DropTable(
                name: "ColIndicator");

            migrationBuilder.DropTable(
                name: "ColDomain");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infobase.Migrations
{
    public partial class sefesrgfqE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Master",
                columns: table => new
                {
                    Index = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Aggregator = table.Column<bool>(nullable: false),
                    Included = table.Column<bool>(nullable: false),
                    CVWarnAt = table.Column<double>(nullable: true),
                    CVSuppressAt = table.Column<double>(nullable: true),
                    ValueAverage = table.Column<double>(nullable: true),
                    ValueUpper = table.Column<double>(nullable: true),
                    ValueLower = table.Column<double>(nullable: true),
                    CVInterpretation = table.Column<int>(nullable: false),
                    CVValue = table.Column<int>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ActivityNameEn = table.Column<string>(nullable: true),
                    IndicatorGroupNameEn = table.Column<string>(nullable: true),
                    LifeCourseNameEn = table.Column<string>(nullable: true),
                    IndicatorNameEn = table.Column<string>(nullable: true),
                    MeasureNameIndexEn = table.Column<string>(nullable: true),
                    MeasureNameDataToolEn = table.Column<string>(nullable: true),
                    MeasureAdditionalRemarksEn = table.Column<string>(nullable: true),
                    MeasureDataAvailableEn = table.Column<string>(nullable: true),
                    MeasureDefinitionEn = table.Column<string>(nullable: true),
                    MeasureMethodEn = table.Column<string>(nullable: true),
                    MeasurePopulationGroupEn = table.Column<string>(nullable: true),
                    MeasureSourceLongEn = table.Column<string>(nullable: true),
                    MeasureSourceShortEn = table.Column<string>(nullable: true),
                    MeasureUnitLongEn = table.Column<string>(nullable: true),
                    MeasureUnitShortEn = table.Column<string>(nullable: true),
                    StrataNameEn = table.Column<string>(nullable: true),
                    StrataNotesEn = table.Column<string>(nullable: true),
                    StrataPopulationTitleFragmentEn = table.Column<string>(nullable: true),
                    StrataSourceEn = table.Column<string>(nullable: true),
                    PointLabelEn = table.Column<string>(nullable: true),
                    PointTextEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Index);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Master");
        }
    }
}

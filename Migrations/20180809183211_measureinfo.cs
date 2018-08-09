using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class measureinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        name: "FK_MeasureAdditionalRemarksTranslation_Translation_TranslationId",
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

            migrationBuilder.CreateIndex(
                name: "IX_MeasureAdditionalRemarksTranslation_MeasureId",
                table: "MeasureAdditionalRemarksTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureDataAvailableTranslation_MeasureId",
                table: "MeasureDataAvailableTranslation",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureMethodTranslation_MeasureId",
                table: "MeasureMethodTranslation",
                column: "MeasureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasureAdditionalRemarksTranslation");

            migrationBuilder.DropTable(
                name: "MeasureDataAvailableTranslation");

            migrationBuilder.DropTable(
                name: "MeasureMethodTranslation");
        }
    }
}

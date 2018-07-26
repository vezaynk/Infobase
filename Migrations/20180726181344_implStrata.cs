using Microsoft.EntityFrameworkCore.Migrations;

namespace ReactDotNetDemo.Migrations
{
    public partial class implStrata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "StrataPopulationTranslation");

            migrationBuilder.DropTable(
                name: "StrataSourceTranslation");
        }
    }
}

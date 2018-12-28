using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infobase.Migrations
{
    public partial class autoincr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndicatorGroupNameTranslation_IndicatorGroup_IndicatorGroupId",
                table: "IndicatorGroupNameTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasureAdditionalRemarksTranslation_Translation_TranslationId",
                table: "MeasureAdditionalRemarksTranslation");

            migrationBuilder.AlterColumn<int>(
                name: "TranslationId",
                table: "Translation",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "StrataId",
                table: "Strata",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "PointId",
                table: "Point",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MeasureId",
                table: "Measure",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "LifeCourseId",
                table: "LifeCourse",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "IndicatorGroupId",
                table: "IndicatorGroup",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "IndicatorId",
                table: "Indicator",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "Activity",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_IndicatorGroupNameTranslation_IndicatorGroup_IndicatorGroup~",
                table: "IndicatorGroupNameTranslation",
                column: "IndicatorGroupId",
                principalTable: "IndicatorGroup",
                principalColumn: "IndicatorGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasureAdditionalRemarksTranslation_Translation_Translation~",
                table: "MeasureAdditionalRemarksTranslation",
                column: "TranslationId",
                principalTable: "Translation",
                principalColumn: "TranslationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndicatorGroupNameTranslation_IndicatorGroup_IndicatorGroup~",
                table: "IndicatorGroupNameTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasureAdditionalRemarksTranslation_Translation_Translation~",
                table: "MeasureAdditionalRemarksTranslation");

            migrationBuilder.AlterColumn<int>(
                name: "TranslationId",
                table: "Translation",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "StrataId",
                table: "Strata",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "PointId",
                table: "Point",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "MeasureId",
                table: "Measure",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "LifeCourseId",
                table: "LifeCourse",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "IndicatorGroupId",
                table: "IndicatorGroup",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "IndicatorId",
                table: "Indicator",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ActivityId",
                table: "Activity",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_IndicatorGroupNameTranslation_IndicatorGroup_IndicatorGroupId",
                table: "IndicatorGroupNameTranslation",
                column: "IndicatorGroupId",
                principalTable: "IndicatorGroup",
                principalColumn: "IndicatorGroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasureAdditionalRemarksTranslation_Translation_TranslationId",
                table: "MeasureAdditionalRemarksTranslation",
                column: "TranslationId",
                principalTable: "Translation",
                principalColumn: "TranslationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class Measuretopoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Strata_Point_DefaultPointId",
                table: "Strata");

            migrationBuilder.DropIndex(
                name: "IX_Strata_DefaultPointId",
                table: "Strata");

            migrationBuilder.DropColumn(
                name: "DefaultPointId",
                table: "Strata");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Point",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Point");

            migrationBuilder.AddColumn<int>(
                name: "DefaultPointId",
                table: "Strata",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Strata_DefaultPointId",
                table: "Strata",
                column: "DefaultPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Strata_Point_DefaultPointId",
                table: "Strata",
                column: "DefaultPointId",
                principalTable: "Point",
                principalColumn: "PointId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

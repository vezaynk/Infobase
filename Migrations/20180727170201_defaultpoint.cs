using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class defaultpoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}

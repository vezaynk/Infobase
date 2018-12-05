using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class aggregator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aggregator",
                table: "Measure",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aggregator",
                table: "Measure");
        }
    }
}

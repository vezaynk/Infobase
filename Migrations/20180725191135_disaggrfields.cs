using Microsoft.EntityFrameworkCore.Migrations;

namespace ReactDotNetDemo.Migrations
{
    public partial class disaggrfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CVValue",
                table: "Point",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "CVSuppressAt",
                table: "Measure",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CVWarnAt",
                table: "Measure",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVValue",
                table: "Point");

            migrationBuilder.DropColumn(
                name: "CVSuppressAt",
                table: "Measure");

            migrationBuilder.DropColumn(
                name: "CVWarnAt",
                table: "Measure");
        }
    }
}

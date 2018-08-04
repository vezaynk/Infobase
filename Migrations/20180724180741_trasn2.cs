using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class trasn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Short",
                table: "Translation",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Long",
                table: "Translation",
                newName: "Text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Translation",
                newName: "Short");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Translation",
                newName: "Long");
        }
    }
}

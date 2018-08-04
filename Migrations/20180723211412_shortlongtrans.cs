using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class shortlongtrans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Translation",
                newName: "Short");

            migrationBuilder.AddColumn<string>(
                name: "Long",
                table: "Translation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Long",
                table: "Translation");

            migrationBuilder.RenameColumn(
                name: "Short",
                table: "Translation",
                newName: "Text");
        }
    }
}

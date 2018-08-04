using Microsoft.EntityFrameworkCore.Migrations;

namespace Infobase.Migrations
{
    public partial class allownull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CVValue",
                table: "Point",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "CVWarnAt",
                table: "Measure",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "CVSuppressAt",
                table: "Measure",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CVValue",
                table: "Point",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CVWarnAt",
                table: "Measure",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CVSuppressAt",
                table: "Measure",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}

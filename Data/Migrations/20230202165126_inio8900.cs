using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class inio8900 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Research_1",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Research_2",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Research_3",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Research_4",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Research_5",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Research_6",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Research_7",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Research_2",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Research_3",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Research_4",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Research_5",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Research_6",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Research_7",
                table: "Settings");

            migrationBuilder.AlterColumn<int>(
                name: "Research_1",
                table: "Settings",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

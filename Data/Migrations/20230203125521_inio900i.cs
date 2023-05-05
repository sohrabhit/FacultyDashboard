using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class inio900i : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Research_1",
                table: "Settings");

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

            migrationBuilder.RenameColumn(
                name: "Research_7",
                table: "Settings",
                newName: "Research_Count");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Research_Count",
                table: "Settings",
                newName: "Research_7");

            migrationBuilder.AddColumn<string>(
                name: "Research_1",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

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
        }
    }
}

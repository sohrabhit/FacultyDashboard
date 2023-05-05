using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ini0099 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "IT_7s");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "IT_6s");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "IT_5s");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "IT_7s",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "IT_6s",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "IT_5s",
                type: "int",
                nullable: true);
        }
    }
}

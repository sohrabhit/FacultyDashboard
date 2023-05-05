using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class iniui800 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Resource_Type",
                table: "Research_1s",
                type: "tinyint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resource_Type",
                table: "Research_1s");
        }
    }
}

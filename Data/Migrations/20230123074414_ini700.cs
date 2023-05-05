using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ini700 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Education_6s");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Education_6s",
                newName: "StudentCount");

            migrationBuilder.AddColumn<int>(
                name: "MasterCount",
                table: "Education_6s",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterCount",
                table: "Education_6s");

            migrationBuilder.RenameColumn(
                name: "StudentCount",
                table: "Education_6s",
                newName: "Count");

            migrationBuilder.AddColumn<byte>(
                name: "Gender",
                table: "Education_6s",
                type: "tinyint",
                nullable: true);
        }
    }
}

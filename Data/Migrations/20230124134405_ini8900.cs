using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ini8900 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Student_8s");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Student_7s");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Student_6s");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Gender",
                table: "Student_8s",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Gender",
                table: "Student_7s",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Gender",
                table: "Student_6s",
                type: "tinyint",
                nullable: true);
        }
    }
}

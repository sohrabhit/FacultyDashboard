using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ini8709 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level_name",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Month_1",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "State_5",
                table: "Settings",
                newName: "Resource_7_All");

            migrationBuilder.RenameColumn(
                name: "State_4",
                table: "Settings",
                newName: "Resource_7_4");

            migrationBuilder.RenameColumn(
                name: "State_3",
                table: "Settings",
                newName: "Resource_7_3");

            migrationBuilder.RenameColumn(
                name: "State_2",
                table: "Settings",
                newName: "Resource_7_2");

            migrationBuilder.RenameColumn(
                name: "State_1",
                table: "Settings",
                newName: "Resource_7_1");

            migrationBuilder.RenameColumn(
                name: "Month_9",
                table: "Settings",
                newName: "Research_1");

            migrationBuilder.RenameColumn(
                name: "Month_8",
                table: "Settings",
                newName: "Education_2_All");

            migrationBuilder.RenameColumn(
                name: "Month_7",
                table: "Settings",
                newName: "Education_2_4");

            migrationBuilder.RenameColumn(
                name: "Month_6",
                table: "Settings",
                newName: "Education_2_3");

            migrationBuilder.RenameColumn(
                name: "Month_5",
                table: "Settings",
                newName: "Education_2_2");

            migrationBuilder.RenameColumn(
                name: "Month_4",
                table: "Settings",
                newName: "Education_2_1");

            migrationBuilder.RenameColumn(
                name: "Month_3",
                table: "Settings",
                newName: "Education_1_All");

            migrationBuilder.RenameColumn(
                name: "Month_2",
                table: "Settings",
                newName: "Education_1_4");

            migrationBuilder.RenameColumn(
                name: "Month_12",
                table: "Settings",
                newName: "Education_1_3");

            migrationBuilder.RenameColumn(
                name: "Month_11",
                table: "Settings",
                newName: "Education_1_2");

            migrationBuilder.RenameColumn(
                name: "Month_10",
                table: "Settings",
                newName: "Education_1_1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Resource_7_All",
                table: "Settings",
                newName: "State_5");

            migrationBuilder.RenameColumn(
                name: "Resource_7_4",
                table: "Settings",
                newName: "State_4");

            migrationBuilder.RenameColumn(
                name: "Resource_7_3",
                table: "Settings",
                newName: "State_3");

            migrationBuilder.RenameColumn(
                name: "Resource_7_2",
                table: "Settings",
                newName: "State_2");

            migrationBuilder.RenameColumn(
                name: "Resource_7_1",
                table: "Settings",
                newName: "State_1");

            migrationBuilder.RenameColumn(
                name: "Research_1",
                table: "Settings",
                newName: "Month_9");

            migrationBuilder.RenameColumn(
                name: "Education_2_All",
                table: "Settings",
                newName: "Month_8");

            migrationBuilder.RenameColumn(
                name: "Education_2_4",
                table: "Settings",
                newName: "Month_7");

            migrationBuilder.RenameColumn(
                name: "Education_2_3",
                table: "Settings",
                newName: "Month_6");

            migrationBuilder.RenameColumn(
                name: "Education_2_2",
                table: "Settings",
                newName: "Month_5");

            migrationBuilder.RenameColumn(
                name: "Education_2_1",
                table: "Settings",
                newName: "Month_4");

            migrationBuilder.RenameColumn(
                name: "Education_1_All",
                table: "Settings",
                newName: "Month_3");

            migrationBuilder.RenameColumn(
                name: "Education_1_4",
                table: "Settings",
                newName: "Month_2");

            migrationBuilder.RenameColumn(
                name: "Education_1_3",
                table: "Settings",
                newName: "Month_12");

            migrationBuilder.RenameColumn(
                name: "Education_1_2",
                table: "Settings",
                newName: "Month_11");

            migrationBuilder.RenameColumn(
                name: "Education_1_1",
                table: "Settings",
                newName: "Month_10");

            migrationBuilder.AddColumn<string>(
                name: "Level_name",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Month_1",
                table: "Settings",
                type: "int",
                nullable: true);
        }
    }
}

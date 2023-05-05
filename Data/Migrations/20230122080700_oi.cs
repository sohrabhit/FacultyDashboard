using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class oi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Education_1s",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}

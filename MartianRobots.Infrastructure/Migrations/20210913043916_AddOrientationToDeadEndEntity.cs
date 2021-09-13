using Microsoft.EntityFrameworkCore.Migrations;

namespace MartianRobots.Infrastructure.Migrations
{
    public partial class AddOrientationToDeadEndEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Orientation",
                table: "DeadEnds",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Orientation",
                table: "DeadEnds");
        }
    }
}

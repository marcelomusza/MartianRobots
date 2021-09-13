using Microsoft.EntityFrameworkCore.Migrations;

namespace MartianRobots.Infrastructure.Migrations
{
    public partial class AddedGridCoordEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GridCoordinatesId",
                table: "RobotMovements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GridCoordinates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GridCoordinates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RobotMovements_GridCoordinatesId",
                table: "RobotMovements",
                column: "GridCoordinatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_RobotMovements_GridCoordinates_GridCoordinatesId",
                table: "RobotMovements",
                column: "GridCoordinatesId",
                principalTable: "GridCoordinates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RobotMovements_GridCoordinates_GridCoordinatesId",
                table: "RobotMovements");

            migrationBuilder.DropTable(
                name: "GridCoordinates");

            migrationBuilder.DropIndex(
                name: "IX_RobotMovements_GridCoordinatesId",
                table: "RobotMovements");

            migrationBuilder.DropColumn(
                name: "GridCoordinatesId",
                table: "RobotMovements");
        }
    }
}

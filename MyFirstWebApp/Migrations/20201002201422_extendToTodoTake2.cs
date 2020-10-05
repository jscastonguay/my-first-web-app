using Microsoft.EntityFrameworkCore.Migrations;

namespace MyFirstWebApp.Migrations
{
    public partial class extendToTodoTake2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Counter",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "etiquette",
                table: "Counter",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "Counter",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Counter");

            migrationBuilder.DropColumn(
                name: "etiquette",
                table: "Counter");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "Counter");
        }
    }
}

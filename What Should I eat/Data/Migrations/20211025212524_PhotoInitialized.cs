using Microsoft.EntityFrameworkCore.Migrations;

namespace What_Should_I_eat.Data.Migrations
{
    public partial class PhotoInitialized : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Ingredients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cuisines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Cuisines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Continents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cuisines");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Cuisines");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Continents");
        }
    }
}

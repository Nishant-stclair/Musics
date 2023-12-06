using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSongs.Migrations
{
    public partial class ColumnProductoinRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Production",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Songs",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Production",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Songs");
        }
    }
}

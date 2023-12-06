using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSongs.Migrations
{
    public partial class NewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SingerName",
                table: "Songs",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SingerName",
                table: "Songs");
        }
    }
}

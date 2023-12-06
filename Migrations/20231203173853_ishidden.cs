using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSongs.Migrations
{
    public partial class ishidden : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "Songs",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "Songs");
        }
    }
}

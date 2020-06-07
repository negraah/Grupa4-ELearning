using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Learning.Data.Migrations
{
    public partial class m9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RepeatPassword",
                table: "Korisnik",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepeatPassword",
                table: "Korisnik");
        }
    }
}

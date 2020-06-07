using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Learning.Data.Migrations
{
    public partial class m8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzije_Kviz_KorisnikId",
                table: "Recenzije");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzije_Korisnik_KorisnikId",
                table: "Recenzije",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzije_Korisnik_KorisnikId",
                table: "Recenzije");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzije_Kviz_KorisnikId",
                table: "Recenzije",
                column: "KorisnikId",
                principalTable: "Kviz",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

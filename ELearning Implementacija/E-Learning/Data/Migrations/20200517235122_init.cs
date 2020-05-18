using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Learning.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oblast",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oblast", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kviz",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rezultat = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kviz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kviz_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kurs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    OblastId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kurs_Oblast_OblastId",
                        column: x => x.OblastId,
                        principalTable: "Oblast",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lekcija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    KursId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekcija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lekcija_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pitanje",
                columns: table => new
                {
                    PitanjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PitanjeTekst = table.Column<string>(nullable: true),
                    TacanOdg = table.Column<string>(nullable: true),
                    NetacenOdg1 = table.Column<string>(nullable: true),
                    NetacenOdg2 = table.Column<string>(nullable: true),
                    NetacenOdg3 = table.Column<string>(nullable: true),
                    KursId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pitanje", x => x.PitanjeId);
                    table.ForeignKey(
                        name: "FK_Pitanje_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Upisivanje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    KursId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upisivanje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Upisivanje_Korisnik_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Upisivanje_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Odgovor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JeLiTacno = table.Column<bool>(nullable: false),
                    PitanjeId = table.Column<int>(nullable: false),
                    KvizId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odgovor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odgovor_Kviz_KvizId",
                        column: x => x.KvizId,
                        principalTable: "Kviz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odgovor_Pitanje_PitanjeId",
                        column: x => x.PitanjeId,
                        principalTable: "Pitanje",
                        principalColumn: "PitanjeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kurs_OblastId",
                table: "Kurs",
                column: "OblastId");

            migrationBuilder.CreateIndex(
                name: "IX_Kviz_KorisnikId",
                table: "Kviz",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Lekcija_KursId",
                table: "Lekcija",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_Odgovor_KvizId",
                table: "Odgovor",
                column: "KvizId");

            migrationBuilder.CreateIndex(
                name: "IX_Odgovor_PitanjeId",
                table: "Odgovor",
                column: "PitanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pitanje_KursId",
                table: "Pitanje",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_Upisivanje_KorisnikId",
                table: "Upisivanje",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Upisivanje_KursId",
                table: "Upisivanje",
                column: "KursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lekcija");

            migrationBuilder.DropTable(
                name: "Odgovor");

            migrationBuilder.DropTable(
                name: "Upisivanje");

            migrationBuilder.DropTable(
                name: "Kviz");

            migrationBuilder.DropTable(
                name: "Pitanje");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Kurs");

            migrationBuilder.DropTable(
                name: "Oblast");
        }
    }
}

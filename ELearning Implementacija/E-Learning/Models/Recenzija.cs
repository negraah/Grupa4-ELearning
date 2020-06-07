using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Learning.Models
{
    public class Recenzija
    {
        public int Id { get; set; }
        public int Ocjena { get; set; }

        public string Komentar { get; set; }

        public int KursId { get; set; }
        public Kurs Kurs { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}

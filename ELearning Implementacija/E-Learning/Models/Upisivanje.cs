using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Learning.Models
{
    public class Upisivanje
    {

        [Key]
        public int Id { get; set; }

        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        public int KursId { get; set; }
        public Kurs Kurs { get; set; }
    }
}

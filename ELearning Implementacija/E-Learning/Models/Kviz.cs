using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
	public class Kviz
	{

		[Key]
		public int Id { get; set; }
		public int Rezultat { get; set; }


		public int KorisnikId { get; set; }
		public Korisnik Korisnik { get; set; }

		/*public int LekcijaId { get; set; }
		public Lekcija Lekcija { get; set; }*/

		/*public int KursId { get; set; }
		public Kurs Kurs { get; set; }
		*/
	}
}

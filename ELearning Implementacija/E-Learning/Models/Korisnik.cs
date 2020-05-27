using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
	public class Korisnik
	{
		[Key]
		public int Id { get; set; }
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		// 0 - obicni, 1 - faks mejl, 2 - admin
		public int Pristup { get; set; }
		

		public List<Upisivanje> Upisivanje { get; set; }
	}
}

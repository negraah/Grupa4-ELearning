using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
	public class Kurs
	{
		[Key]
		public int Id { get; set; }
		public string Naziv { get; set; }

		public bool PotrebanFaks { get; set; }

		public int OblastId { get; set; }
		public Oblast Oblast { get; set; }

		public List<Lekcija> Lekcije { get; set; }
		public List<Upisivanje> Upisivanje { get; set; }

		public string Opis { get; set; }
    }
}
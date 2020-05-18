using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
	public class Pitanje
	{
		[Key]
		public int PitanjeId { get; set; }
		public string PitanjeTekst { get; set; }
		public string TacanOdg { get; set; }
		public string NetacenOdg1 { get; set; }
		public string NetacenOdg2 { get; set; }
		public string NetacenOdg3 { get; set; }

		/*public int LekcijaId { get; set; }
		public Lekcija Lekcija { get; set; }
		*/

		public int KursId { get; set; }
		public Kurs Kurs { get; set; }
	}
}
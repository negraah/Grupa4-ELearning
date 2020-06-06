using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
	public class Oblast
	{


		[Key]
		public int Id { get; set; }
		public string Naziv { get; set; }

		public string Opis { get; set; }

		public List<Kurs> Kurs { get; set; }
	}
}
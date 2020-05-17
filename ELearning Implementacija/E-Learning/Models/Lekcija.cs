using System;
using System.ComponentModel.DataAnnotations;

namespace E_Learning.Models
{
	public class Lekcija
	{
		[Key]
		public int Id { get; set; }
		public string Ime { get; set; }

		public int KursId { get; set; }
		public Kurs Kurs { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.Globalization;

namespace E_Learning.Models
{
	public class Ljestvica
	{
		private List<Korisnik> topKorisnici;
		private Kurs kurs;

		public Ljestvica(List<Korisnik> topKorisnici, Kurs kurs)
		{
			this.topKorisnici = topKorisnici;
			this.kurs = kurs;
		}
	}
}
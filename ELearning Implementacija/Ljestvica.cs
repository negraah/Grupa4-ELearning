using System;
using System.Collections.Generic;
using System.Globalization;

namespace ELearning
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
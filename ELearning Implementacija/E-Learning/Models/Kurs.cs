using System;
using System.Collections.Generic;

namespace ELearning
{
	public class Kurs
	{
		private String naziv;
		private List<Lekcija> lekcije;
		private List<Korisnik> upisani;

		public Kurs(String naziv, List<Lekcija> lekcije, List<Korisnik> upisani)
		{
			this.naziv = naziv;
			this.lekcije = lekcije;
			this.upisani = upisani;
		}
	}
}
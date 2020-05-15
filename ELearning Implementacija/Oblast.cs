using System;
using System.Collections.Generic;

namespace ELearning
{
	public class Oblast
	{
		private String naziv;
		private List<Kurs> kursevi;

		public Oblast(String naziv, List<Kurs> kursevi)
		{
			this.naziv = naziv;
			this.kursevi = kursevi;
		}
	}
}
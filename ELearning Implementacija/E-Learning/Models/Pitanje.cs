using System;
using System.Collections.Generic;

namespace E_Learning.Models
{
	public class Pitanje
	{
		private String pitanje;
		private List<String> odgovori;
		private int tacanOdgovorIndeks;
		private int trenutniOdgovor;

		public Pitanje(String pitanje, List<String> odgovori, int tacanOdgovorIndeks)
		{
			this.pitanje = pitanje;
			this.odgovori = odgovori;
			this.tacanOdgovorIndeks = tacanOdgovorIndeks;
			this.tacanOdgovorIndeks = -1;
		}
	}
}
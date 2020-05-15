using System;
using System.Collections.Generic;

namespace ELearning
{
	public class Korisnik
	{
		private int id;
		private String ime;
		private String prezime;
		private String email;
		private List<Kurs> kursevi;

		public Korisnik(int id, String ime, String prezime, String email)
		{
			this.id = id;
			this.ime = ime;
			this.email = email;
			this.prezime = prezime;
			this.kursevi = new List<Kurs>();
		}

		public int getId() => id;

		public void setId(int id) => this.id = id;

		public String GetIme() => ime;

		public void setIme(String ime) => this.ime = ime;

		public String getPrezime() => prezime;

		public void setPrezime(String prezime) => this.prezime = prezime;

		public String getEmail() => email;

		public void setEmail(String email) => this.email= email;

		public List<Kurs> getKurs() => kursevi;

		public void upisiKurs(Kurs k)
		{
			kursevi.Add(k);
		}

	}
}

# Grupa4-ELearning
***Tema: Platforma za učenje***

***Članovi tima*** 
-------------

- Negra Ahmetspahić
- Adnan Šabanović
- Muhamed Opačin

***Opis teme***
-------------
>ELearning je aplikacija sa ugrađenom platformom za učenje. Aplikacija sadrži oblasti koje su podijeljene u kurseve. U zavisnosti od toga da li korisnik pristupa aplikaciji sa fakultetskim e-mailom ili ne, otvara/ne otvara mu se mogućnost da pohađa dodatne kurseve.
Moguć je pristup više korisnika. Korisnik također može izabrati oblasti, a i kurseve koje će pohađati. Glavne obaveze aplikacije su da snabdijeva korisnika informativnim sadržajem na razne načine i također vrši provjeravanje usvojenog znanja putem kvizova. Aplikacija također prati i informiše korisnika o napretku u određenim oblastima. Korisnik je također u mogućnosti da ocjenjuje kurs koji je pohađao. Svaki dan korisnik može pristupiti dnevnom izazovu ( daily challenge. ) Ukoliko korisnik želi pristupiti navedenom izazovu, dobija 3 pitanja iz svakog kursa koji pohađa, te nakon odrađenog izazova može vidjeti rang listu svih korisnika tog kursa koji su pristupili dnevnom izazovu. Primarni cilj aplikacije jeste usvajanje novog znanja, a također i vještina koje korisnik stiče.


***Akteri***
-------------
***Primarni tip aktera***
- Posjetilac 
- Korisnik
- Korisnik aplikacije bez fakultetskog e-maila 
- Korisnik aplikacije sa fakultetskim e-mailom 
- Administrator 

***Drugi tip aktera***
- Eksterna aplikacija za provjeravanje, odnosno validaciju e-mail adrese i provjera da li adresa pripada studentu

***Funkcionalnost***
------------- 

+ Posjetilac
  + Mogućnost prijavljivanja na sistem
  + Mogućnost kreiranja korisničkog računa
  + Moguć pregled oblasti
  + Moguć pregled kurseva
  
+ Korisnik 
  + Mogućnost prijavljivanja na sistem
  + Mogućnost kreiranja korisničkog računa
  + Moguć pregled oblasti
  + Moguć pregled kurseva
  + Mogućnost upisivanja kursa
  + Pristupanje lekcijama
  + Izrada kviza za lekciju
  + Izrada kviza za kurs
  + Mogućnost dodjeljivanja ocjene kursu
  + Pristup dnevnom izazovu
  + Mogućnost pregledanja ljestvice kursa kojeg pohađa
  + Ostavljanje komentara na kursu
  + Pregled dosadašnjeg uspjeha
  + Mogućnost odjavljivanja sa sistema
 
  
+ Korisnik aplikacije bez fakultetskog e-maila
  + Sve funkcionalnosti korisnika

+ Korisnik aplikacije sa fakultetskim e-mailom  
  + Sve funkcionalnosti korisnika

+ Administrator
  + Sve funkcionalnosti korisnika
  + Može pregledati statistiku svih korisnika
 
***Web servis***
-------------

Dnevni izazov (daily challenge) je planiran da bude web servis. Ova funkcionalnost služi za određivanje najboljih korisnika koji će se prikazivati na ljestvici. Ideja je da server prvo koristeći internu funkciju generiše, odnosno izračuna random seed koji je za svaka dva dana različit, ali za svaka dva trenutka u istom danu identičan. Nakon što generiše seed, generišu se pitanja koja će se postaviti korisnicima. Zbog ovoga, kad god neki korisnik bude htio da pristupi daily challenge, on će ustvari preuzimati pitanja za dati kviz sa servera, a ne da ih on sam generiše, kao što se radi kod običnog kviza.

***Video naše aplikacije***
-------------
https://drive.google.com/file/d/1_bF9gMO5oW_30ToEpZTIESLgfiC3XItw/view?usp=sharing
Da bi video stao u vremenske zahtjeve može se slušati sa brzinom x2.

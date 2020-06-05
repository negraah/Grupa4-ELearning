using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Learning.Data;
using E_Learning.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Learning.Controllers
{
    public class KvizsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static List<Pitanje> pitanja;
        private static Random rng = new Random();
        private static List<List<String>> odgovori_tekst = null;

        public KvizsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public static List<T> Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        // GET: Kvizs
        public async Task<IActionResult> Index()
        {
            if(KvizsController.pitanja != null) KvizsController.pitanja.ForEach(x => Console.WriteLine(x.PitanjeTekst));
            var applicationDbContext = _context.Kviz.Include(k => k.Korisnik);
            return View(await applicationDbContext.ToListAsync());
        }


        //GET: Izrada
        public IActionResult Izrada()
        {
            if (KvizsController.pitanja != null) KvizsController.pitanja.ForEach(x => Console.WriteLine(x.PitanjeTekst));
            odgovori_tekst = new List<List<String>>();
            for (int i = 0; i < 3; i++)
            {
                var odgovori_i = new List<String>();
                odgovori_i.Add(pitanja[i].TacanOdg);
                odgovori_i.Add(pitanja[i].NetacenOdg1);
                odgovori_i.Add(pitanja[i].NetacenOdg2);
                odgovori_i.Add(pitanja[i].NetacenOdg3);
                odgovori_tekst.Add(Shuffle(odgovori_i));
            }
            
            return View();
        }

        public static List<List<String>> get_odgovori_pitanja()
        {
            return odgovori_tekst;
        }

        // GET: Kvizs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kviz = await _context.Kviz
                .Include(k => k.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kviz == null)
            {
                return NotFound();
            }

            return View(kviz);
        }

        // GET: Kvizs/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id");
            return View();
        }

        // POST: Kvizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rezultat,KorisnikId")] Kviz kviz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kviz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", kviz.KorisnikId);
            return View(kviz);
        }

        // GET: Kvizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kviz = await _context.Kviz.FindAsync(id);
            if (kviz == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", kviz.KorisnikId);
            return View(kviz);
        }

        // POST: Kvizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rezultat,KorisnikId")] Kviz kviz)
        {
            if (id != kviz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kviz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KvizExists(kviz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", kviz.KorisnikId);
            return View(kviz);
        }

        // GET: Kvizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kviz = await _context.Kviz
                .Include(k => k.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kviz == null)
            {
                return NotFound();
            }

            return View(kviz);
        }

        // POST: Kvizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kviz = await _context.Kviz.FindAsync(id);
            _context.Kviz.Remove(kviz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KvizExists(int id)
        {
            return _context.Kviz.Any(e => e.Id == id);
        }

        // POST: Predaj
        [HttpPost]
        public async Task<RedirectToActionResult> Predaj(string pitanje_0, string pitanje_1, string pitanje_2)
        {
            Console.WriteLine("EV GA");
            Console.WriteLine(pitanje_0);
            Console.WriteLine(pitanje_1);
            Console.WriteLine(pitanje_2);
            Console.WriteLine("TU JE");

            int bodovi = 0;
            if (pitanja[0].TacanOdg == pitanje_0) bodovi++;
            if (pitanja[1].TacanOdg == pitanje_1) bodovi++;
            if (pitanja[2].TacanOdg == pitanje_2) bodovi++;

            if (ModelState.IsValid)
            {
                Odgovor odg_0 = new Odgovor(), odg_1 = new Odgovor(), odg_2 = new Odgovor();
                Kviz k = new Kviz();
                odg_0.Kviz = k;
                odg_1.Kviz = k;
                odg_2.Kviz = k;
                /*
                Kviz dummy_k = new Kviz(); dummy_k.Id = 0;
                Odgovor dummy_o = new Odgovor(); dummy_o.Id = 0;
                
                int id = _context.Kviz.ToArray().Aggregate(seed: dummy_k, func: ((k1, k2) => k1.Id > k2.Id ? k1 : k2)).Id + 1;
                k.Id = id;
                odg_0.KvizId = id;
                odg_1.KvizId = id;
                odg_2.KvizId = id;

                id = _context.Odgovor.ToArray().Aggregate(seed: dummy_o, func: ((k1, k2) => k1.Id > k2.Id ? k1 : k2)).Id + 1;
                odg_0.Id = id + 0;
                odg_1.Id = id + 1;
                odg_2.Id = id + 2;
                */
                k.Rezultat = bodovi;
                k.KorisnikId = KorisniksController.Trenutni.Id;

                odg_0.PitanjeId = pitanja[0].PitanjeId;
                odg_1.PitanjeId = pitanja[1].PitanjeId;
                odg_2.PitanjeId = pitanja[2].PitanjeId;

                odg_0.JeLiTacno = pitanja[0].TacanOdg == pitanje_0;
                odg_1.JeLiTacno = pitanja[1].TacanOdg == pitanje_1;
                odg_2.JeLiTacno = pitanja[2].TacanOdg == pitanje_2;

                _context.Add(k);
                await _context.SaveChangesAsync();

                _context.Add(odg_0);
                _context.Add(odg_1);
                _context.Add(odg_2);
                await _context.SaveChangesAsync();

            }
            //ViewData["OblastId"] = new SelectList(_context.Oblast, "Id", "Id", kurs.OblastId);
            return RedirectToAction("Index", "Kvizs");
        }
    }
}

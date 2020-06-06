using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Learning.Data;
using E_Learning.Models;

namespace E_Learning.Controllers
{
    public class LekcijasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static Kurs trenutniKurs = null;

        public LekcijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lekcijas
        public async Task<IActionResult> Index(int? KursId)
        {
            var applicationDbContext = _context.Lekcija.Include(l => l.Kurs);
            if (!KursId.HasValue)
            {
                trenutniKurs = null;
                return View(await applicationDbContext.ToListAsync());
            }
            trenutniKurs = _context.Kurs.Find(KursId);
            return View(await applicationDbContext.Where(lekcija => lekcija.KursId == KursId).ToListAsync());
        }

        // GET: Lekcijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lekcija = await _context.Lekcija
                .Include(l => l.Kurs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lekcija == null)
            {
                return NotFound();
            }

            return View(lekcija);
        }

        // GET: Lekcijas/Create
        public IActionResult Create()
        {
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id");
            return View();
        }

        // POST: Lekcijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,KursId")] Lekcija lekcija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lekcija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", lekcija.KursId);
            return View(lekcija);
        }

        // GET: Lekcijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lekcija = await _context.Lekcija.FindAsync(id);
            if (lekcija == null)
            {
                return NotFound();
            }
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", lekcija.KursId);
            return View(lekcija);
        }

        // POST: Lekcijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,KursId")] Lekcija lekcija)
        {
            if (id != lekcija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lekcija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LekcijaExists(lekcija.Id))
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
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", lekcija.KursId);
            return View(lekcija);
        }

        // GET: Lekcijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lekcija = await _context.Lekcija
                .Include(l => l.Kurs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lekcija == null)
            {
                return NotFound();
            }

            return View(lekcija);
        }

        // POST: Lekcijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lekcija = await _context.Lekcija.FindAsync(id);
            _context.Lekcija.Remove(lekcija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LekcijaExists(int id)
        {
            return _context.Lekcija.Any(e => e.Id == id);
        }



        public async Task<IActionResult> PokreniKviz()
        {
            Console.WriteLine("HI!");
            List<Pitanje> pitanja = await _context.Pitanje.Where(p => p.KursId == trenutniKurs.Id).ToListAsync();
            KvizsController.is_daily = false;
            pitanja = KvizsController.Shuffle(pitanja);
            pitanja = pitanja.GetRange(0, 3);
            KvizsController.pitanja = pitanja;
            return RedirectToAction("Izrada", "Kvizs");
        }


        public async Task<IActionResult> PokreniDaily()
        {
            if(KorisniksController.Trenutni.ZadnjiDaily == KvizsController.getDan())
            {
                return RedirectToAction("VecRadio", "NotUpisan");
            }
            Console.WriteLine("HI!");
            List<Pitanje> pitanja = await _context.Pitanje.Where(p => p.KursId == trenutniKurs.Id).ToListAsync();
            KvizsController.is_daily = true;
            KvizsController.rng = new Random(Seed: KvizsController.getDan());
            KorisniksController.Trenutni.ZadnjiDaily = KvizsController.getDan();
            _context.Update(KorisniksController.Trenutni);
            await _context.SaveChangesAsync();
            pitanja = KvizsController.Shuffle(pitanja);
            pitanja = pitanja.GetRange(0, 3);
            KvizsController.pitanja = pitanja;
            return RedirectToAction("Izrada", "Kvizs");
        }

        public async Task<IActionResult> DajLjestvicu()
        {
            List < Korisnik > korisniks = await _context.Korisnik.ToListAsync();
            List<Tuple<Korisnik, int>> ranking = new List<Tuple<Korisnik, int>>();
            foreach(Korisnik k in korisniks)
            {
                var t = new Tuple<Korisnik, int>(k, DajBodove(k, LekcijasController.trenutniKurs));

                ranking.Add(t);
            }
            return View(ranking);
        }

        public int? DajKursKviza(Kviz k)
        {
            Odgovor o = _context.Odgovor.FirstOrDefault(x => x.Kviz == k & x.Pitanje != null);
            if (o != null)
            {
                Pitanje p = _context.Pitanje.FirstOrDefault(x => x.PitanjeId == o.PitanjeId);
                return p.KursId;
            }
            return null;
        }

        private int DajBodove(Korisnik korisnik, Kurs kurs)
        {
            var x = _context.Kviz.Where(k => k.Korisnik == korisnik /*& DajKursKviza(k) == kurs*/);
            int sum = 0;
            var l = x.ToList();
            foreach (var i in l)
            {
                if(DajKursKviza(i) == kurs.Id)
                    sum += i.Rezultat;
            }


            return sum;
        }
    }
}

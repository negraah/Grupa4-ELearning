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
    public class KvizsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private List<Pitanje> pitanja;
        private static Random rng = new Random();

        public KvizsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public static List<Pitanje> Shuffle(List<Pitanje> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Pitanje value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }


        public static async void ListaPitanja(int KursId, ApplicationDbContext _context)
        {
            List<Pitanje> pitanja = await _context.Pitanje.Where(p => p.KursId == KursId).ToListAsync();
            pitanja = Shuffle(pitanja);
            pitanja = pitanja.GetRange(0, 3);
        }

        public static void ListaPitanja(Kurs k, ApplicationDbContext _context)
        {
            ListaPitanja(k.Id, _context);
        }

        // GET: Kvizs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Kviz.Include(k => k.Korisnik);
            return View(await applicationDbContext.ToListAsync());
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
    }
}

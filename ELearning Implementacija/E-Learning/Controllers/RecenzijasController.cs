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
    public class RecenzijasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecenzijasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recenzijas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Recenzije.Include(r => r.Korisnik).Include(r => r.Kurs);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Recenzijas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzije
                .Include(r => r.Korisnik)
                .Include(r => r.Kurs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // GET: Recenzijas/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Kviz, "Id", "Id");
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id");
            return View();
        }

        // POST: Recenzijas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ocjena,Komentar,KursId,KorisnikId")] Recenzija recenzija)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recenzija);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Kviz, "Id", "Id", recenzija.KorisnikId);
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", recenzija.KursId);
            return View(recenzija);
        }

        // GET: Recenzijas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzije.FindAsync(id);
            if (recenzija == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Kviz, "Id", "Id", recenzija.KorisnikId);
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", recenzija.KursId);
            return View(recenzija);
        }

        // POST: Recenzijas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ocjena,Komentar,KursId,KorisnikId")] Recenzija recenzija)
        {
            if (id != recenzija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recenzija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecenzijaExists(recenzija.Id))
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
            ViewData["KorisnikId"] = new SelectList(_context.Kviz, "Id", "Id", recenzija.KorisnikId);
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", recenzija.KursId);
            return View(recenzija);
        }

        // GET: Recenzijas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzija = await _context.Recenzije
                .Include(r => r.Korisnik)
                .Include(r => r.Kurs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recenzija == null)
            {
                return NotFound();
            }

            return View(recenzija);
        }

        // POST: Recenzijas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recenzija = await _context.Recenzije.FindAsync(id);
            _context.Recenzije.Remove(recenzija);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecenzijaExists(int id)
        {
            return _context.Recenzije.Any(e => e.Id == id);
        }
    }
}

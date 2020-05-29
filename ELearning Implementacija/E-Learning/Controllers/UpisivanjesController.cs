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
    public class UpisivanjesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UpisivanjesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Upisivanjes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Upisivanje.Include(u => u.Korisnik).Include(u => u.Kurs);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Upisivanjes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upisivanje = await _context.Upisivanje
                .Include(u => u.Korisnik)
                .Include(u => u.Kurs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upisivanje == null)
            {
                return NotFound();
            }

            return View(upisivanje);
        }

        // GET: Upisivanjes/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id");
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id");
            return View();
        }

        // POST: Upisivanjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KorisnikId,KursId")] Upisivanje upisivanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(upisivanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", upisivanje.KorisnikId);
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", upisivanje.KursId);
            return View(upisivanje);
        }

        // GET: Upisivanjes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upisivanje = await _context.Upisivanje.FindAsync(id);
            if (upisivanje == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", upisivanje.KorisnikId);
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", upisivanje.KursId);
            return View(upisivanje);
        }

        // POST: Upisivanjes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KorisnikId,KursId")] Upisivanje upisivanje)
        {
            if (id != upisivanje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(upisivanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UpisivanjeExists(upisivanje.Id))
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
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", upisivanje.KorisnikId);
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", upisivanje.KursId);
            return View(upisivanje);
        }

        // GET: Upisivanjes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var upisivanje = await _context.Upisivanje
                .Include(u => u.Korisnik)
                .Include(u => u.Kurs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (upisivanje == null)
            {
                return NotFound();
            }

            return View(upisivanje);
        }

        // POST: Upisivanjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var upisivanje = await _context.Upisivanje.FindAsync(id);
            _context.Upisivanje.Remove(upisivanje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UpisivanjeExists(int id)
        {
            return _context.Upisivanje.Any(e => e.Id == id);
        }
    }
}

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
    public class PitanjesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PitanjesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pitanjes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pitanje.Include(p => p.Kurs);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pitanjes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pitanje = await _context.Pitanje
                .Include(p => p.Kurs)
                .FirstOrDefaultAsync(m => m.PitanjeId == id);
            if (pitanje == null)
            {
                return NotFound();
            }

            return View(pitanje);
        }

        // GET: Pitanjes/Create
        public IActionResult Create()
        {
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id");
            return View();
        }

        // POST: Pitanjes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PitanjeId,PitanjeTekst,TacanOdg,NetacenOdg1,NetacenOdg2,NetacenOdg3,KursId")] Pitanje pitanje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pitanje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", pitanje.KursId);
            return View(pitanje);
        }

        // GET: Pitanjes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pitanje = await _context.Pitanje.FindAsync(id);
            if (pitanje == null)
            {
                return NotFound();
            }
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", pitanje.KursId);
            return View(pitanje);
        }

        // POST: Pitanjes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PitanjeId,PitanjeTekst,TacanOdg,NetacenOdg1,NetacenOdg2,NetacenOdg3,KursId")] Pitanje pitanje)
        {
            if (id != pitanje.PitanjeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pitanje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PitanjeExists(pitanje.PitanjeId))
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
            ViewData["KursId"] = new SelectList(_context.Kurs, "Id", "Id", pitanje.KursId);
            return View(pitanje);
        }

        // GET: Pitanjes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pitanje = await _context.Pitanje
                .Include(p => p.Kurs)
                .FirstOrDefaultAsync(m => m.PitanjeId == id);
            if (pitanje == null)
            {
                return NotFound();
            }

            return View(pitanje);
        }

        // POST: Pitanjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pitanje = await _context.Pitanje.FindAsync(id);
            _context.Pitanje.Remove(pitanje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PitanjeExists(int id)
        {
            return _context.Pitanje.Any(e => e.PitanjeId == id);
        }
    }
}

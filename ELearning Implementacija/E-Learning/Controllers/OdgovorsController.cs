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
    public class OdgovorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OdgovorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Odgovors
        public async Task<IActionResult> Index(int? k)
        {
            var applicationDbContext = _context.Odgovor.Include(o => o.Kviz);
            if (!k.HasValue)
            {
                return View(await applicationDbContext.ToListAsync());
            }
            return View(await applicationDbContext.Where(o => o.KvizId == k).ToListAsync());
        }

        // GET: Odgovors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovor = await _context.Odgovor
                .Include(o => o.Kviz)
                .Include(o => o.Pitanje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odgovor == null)
            {
                return NotFound();
            }

            return View(odgovor);
        }

        // GET: Odgovors/Create
        public IActionResult Create()
        {
            ViewData["KvizId"] = new SelectList(_context.Kviz, "Id", "Id");
            ViewData["PitanjeId"] = new SelectList(_context.Pitanje, "PitanjeId", "PitanjeId");
            return View();
        }

        // POST: Odgovors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JeLiTacno,PitanjeId,KvizId")] Odgovor odgovor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(odgovor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KvizId"] = new SelectList(_context.Kviz, "Id", "Id", odgovor.KvizId);
            ViewData["PitanjeId"] = new SelectList(_context.Pitanje, "PitanjeId", "PitanjeId", odgovor.PitanjeId);
            return View(odgovor);
        }

        // GET: Odgovors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovor = await _context.Odgovor.FindAsync(id);
            if (odgovor == null)
            {
                return NotFound();
            }
            ViewData["KvizId"] = new SelectList(_context.Kviz, "Id", "Id", odgovor.KvizId);
            ViewData["PitanjeId"] = new SelectList(_context.Pitanje, "PitanjeId", "PitanjeId", odgovor.PitanjeId);
            return View(odgovor);
        }

        // POST: Odgovors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JeLiTacno,PitanjeId,KvizId")] Odgovor odgovor)
        {
            if (id != odgovor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(odgovor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OdgovorExists(odgovor.Id))
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
            ViewData["KvizId"] = new SelectList(_context.Kviz, "Id", "Id", odgovor.KvizId);
            ViewData["PitanjeId"] = new SelectList(_context.Pitanje, "PitanjeId", "PitanjeId", odgovor.PitanjeId);
            return View(odgovor);
        }

        // GET: Odgovors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var odgovor = await _context.Odgovor
                .Include(o => o.Kviz)
                .Include(o => o.Pitanje)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (odgovor == null)
            {
                return NotFound();
            }

            return View(odgovor);
        }

        // POST: Odgovors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var odgovor = await _context.Odgovor.FindAsync(id);
            _context.Odgovor.Remove(odgovor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OdgovorExists(int id)
        {
            return _context.Odgovor.Any(e => e.Id == id);
        }
    }
}

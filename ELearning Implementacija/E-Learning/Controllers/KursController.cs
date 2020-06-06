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
    public class KursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kurs
        public async Task<IActionResult> Index(int? OblastId)
        {
            var applicationDbContext = _context.Kurs.Include(k => k.Oblast);
            if (!OblastId.HasValue)
            {
                return View(await applicationDbContext.ToListAsync());
            }
            return View(await applicationDbContext.Where(k => k.OblastId == OblastId).ToListAsync());
        }

        // GET: Kurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurs
                .Include(k => k.Oblast)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        // GET: Kurs/Create
        public IActionResult Create()
        {
            ViewData["OblastId"] = new SelectList(_context.Oblast, "Id", "Id");
            return View();
        }

        // POST: Kurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,PotrebanFaks,OblastId")] Kurs kurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OblastId"] = new SelectList(_context.Oblast, "Id", "Id", kurs.OblastId);
            return View(kurs);
        }

        // GET: Kurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurs.FindAsync(id);
            if (kurs == null)
            {
                return NotFound();
            }
            ViewData["OblastId"] = new SelectList(_context.Oblast, "Id", "Id", kurs.OblastId);
            return View(kurs);
        }

        // POST: Kurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Opis,PotrebanFaks,OblastId")] Kurs kurs)
        {
            if (id != kurs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KursExists(kurs.Id))
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
            ViewData["OblastId"] = new SelectList(_context.Oblast, "Id", "Id", kurs.OblastId);
            return View(kurs);
        }

        // GET: Kurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.Kurs
                .Include(k => k.Oblast)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        // POST: Kurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kurs = await _context.Kurs.FindAsync(id);
            _context.Kurs.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KursExists(int id)
        {
            return _context.Kurs.Any(e => e.Id == id);
        }

        //GET : Kurs
        public async Task<IActionResult> Open(int id)
        {
            Kurs kurs = await _context.Kurs.FirstOrDefaultAsync(k => k.Id == id);
            if(KorisniksController.Trenutni == null) return RedirectToAction("NijePrijavljen", "NotUpisan");
            Upisivanje upisivanje = await _context.Upisivanje.FirstOrDefaultAsync
                (u => u.KorisnikId == KorisniksController.Trenutni.Id & kurs.Id == u.KursId);
            if (upisivanje == null)
            {
                return RedirectToAction("NijeUpisan", "NotUpisan");
            }

            return RedirectToAction("Index", "Lekcijas", new { KursId = id });
        }


        //GET : Upis
        public async Task<IActionResult> Upisi(int id)
        {
            
            Kurs kurs = await _context.Kurs.FirstOrDefaultAsync(k => k.Id == id);
            if (KorisniksController.Trenutni == null) 
                return RedirectToAction("Index", "Kurs", new { OblastId = kurs.OblastId });

            if(kurs.PotrebanFaks & KorisniksController.Trenutni.Pristup == 0)
                return RedirectToAction("Index", "Kurs", new { OblastId = kurs.OblastId });

            Upisivanje upisivanje = new Upisivanje();
            upisivanje.KorisnikId = KorisniksController.Trenutni.Id;
            upisivanje.KursId = kurs.Id;
            
            if(await _context.Upisivanje.FirstOrDefaultAsync(u => u.KorisnikId == KorisniksController.Trenutni.Id & u.KursId == kurs.Id) != null)
                return RedirectToAction("Index", "Kurs", new { OblastId = kurs.OblastId });

            _context.Upisivanje.Add(upisivanje);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Lekcijas", new { KursId = id });
        }
    }
}

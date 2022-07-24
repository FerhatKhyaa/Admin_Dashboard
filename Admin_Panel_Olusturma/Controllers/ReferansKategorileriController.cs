using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin_Panel_Olusturma.Entities;
using Admin_Panel_Olusturma.Identity;

namespace Admin_Panel_Olusturma.Controllers
{
    public class ReferansKategorileriController : Controller
    {
        private readonly FerhatKahyaDbContext _context;

        public ReferansKategorileriController(FerhatKahyaDbContext context)
        {
            _context = context;
        }

        // GET: ReferansKategorileri
        public async Task<IActionResult> Index()
        {
            var ferhatKahyaDbContext = _context.ReferansKategorileri.Include(r => r.ReferansKategori);
            return View(await ferhatKahyaDbContext.ToListAsync());
        }

        // GET: ReferansKategorileri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReferansKategorileri == null)
            {
                return NotFound();
            }

            var referansKategorileri = await _context.ReferansKategorileri
                .Include(r => r.ReferansKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referansKategorileri == null)
            {
                return NotFound();
            }

            return View(referansKategorileri);
        }

        // GET: ReferansKategorileri/Create
        public IActionResult Create()
        {
            ViewData["ReferansId"] = new SelectList(_context.ReferansKategorileri, "Id", "ReferansAdi");
            return View();
        }

        // POST: ReferansKategorileri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReferansAdi,Durum,ReferansId")] ReferansKategorileri referansKategorileri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referansKategorileri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReferansId"] = new SelectList(_context.ReferansKategorileri, "Id", "ReferansAdi", referansKategorileri.ReferansId);
            return View(referansKategorileri);
        }

        // GET: ReferansKategorileri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReferansKategorileri == null)
            {
                return NotFound();
            }

            var referansKategorileri = await _context.ReferansKategorileri.FindAsync(id);
            if (referansKategorileri == null)
            {
                return NotFound();
            }
            ViewData["ReferansId"] = new SelectList(_context.ReferansKategorileri, "Id", "ReferansAdi", referansKategorileri.ReferansId);
            return View(referansKategorileri);
        }

        // POST: ReferansKategorileri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReferansAdi,Durum,ReferansId")] ReferansKategorileri referansKategorileri)
        {
            if (id != referansKategorileri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referansKategorileri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferansKategorileriExists(referansKategorileri.Id))
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
            ViewData["ReferansId"] = new SelectList(_context.ReferansKategorileri, "Id", "ReferansAdi", referansKategorileri.ReferansId);
            return View(referansKategorileri);
        }

        // GET: ReferansKategorileri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReferansKategorileri == null)
            {
                return NotFound();
            }

            var referansKategorileri = await _context.ReferansKategorileri
                .Include(r => r.ReferansKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referansKategorileri == null)
            {
                return NotFound();
            }

            return View(referansKategorileri);
        }

        // POST: ReferansKategorileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReferansKategorileri == null)
            {
                return Problem("Entity set 'FerhatKahyaDbContext.ReferansKategorileri'  is null.");
            }
            var referansKategorileri = await _context.ReferansKategorileri.FindAsync(id);
            if (referansKategorileri != null)
            {
                _context.ReferansKategorileri.Remove(referansKategorileri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferansKategorileriExists(int id)
        {
          return (_context.ReferansKategorileri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

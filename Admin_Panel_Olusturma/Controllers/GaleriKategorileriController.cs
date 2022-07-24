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
    public class GaleriKategorileriController : Controller
    {
        private readonly FerhatKahyaDbContext _context;

        public GaleriKategorileriController(FerhatKahyaDbContext context)
        {
            _context = context;
        }

        // GET: GaleriKategorileri
        public async Task<IActionResult> Index()
        {
            var ferhatKahyaDbContext = _context.GaleriKategorileri.Include(g => g.GaleriKategori);
            return View(await ferhatKahyaDbContext.ToListAsync());
        }

        // GET: GaleriKategorileri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GaleriKategorileri == null)
            {
                return NotFound();
            }

            var galeriKategorileri = await _context.GaleriKategorileri
                .Include(g => g.GaleriKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galeriKategorileri == null)
            {
                return NotFound();
            }

            return View(galeriKategorileri);
        }

        // GET: GaleriKategorileri/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.GaleriKategorileri, "Id", "KategoriAdi");
            return View();
        }

        // POST: GaleriKategorileri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KategoriAdi,Durum,KategoriId")] GaleriKategorileri galeriKategorileri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(galeriKategorileri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.GaleriKategorileri, "Id", "KategoriAdi", galeriKategorileri.KategoriId);
            return View(galeriKategorileri);
        }

        // GET: GaleriKategorileri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GaleriKategorileri == null)
            {
                return NotFound();
            }

            var galeriKategorileri = await _context.GaleriKategorileri.FindAsync(id);
            if (galeriKategorileri == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.GaleriKategorileri, "Id", "KategoriAdi", galeriKategorileri.KategoriId);
            return View(galeriKategorileri);
        }

        // POST: GaleriKategorileri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KategoriAdi,Durum,KategoriId")] GaleriKategorileri galeriKategorileri)
        {
            if (id != galeriKategorileri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(galeriKategorileri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaleriKategorileriExists(galeriKategorileri.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.GaleriKategorileri, "Id", "KategoriAdi", galeriKategorileri.KategoriId);
            return View(galeriKategorileri);
        }

        // GET: GaleriKategorileri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GaleriKategorileri == null)
            {
                return NotFound();
            }

            var galeriKategorileri = await _context.GaleriKategorileri
                .Include(g => g.GaleriKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galeriKategorileri == null)
            {
                return NotFound();
            }

            return View(galeriKategorileri);
        }

        // POST: GaleriKategorileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GaleriKategorileri == null)
            {
                return Problem("Entity set 'FerhatKahyaDbContext.GaleriKategorileri'  is null.");
            }
            var galeriKategorileri = await _context.GaleriKategorileri.FindAsync(id);
            if (galeriKategorileri != null)
            {
                _context.GaleriKategorileri.Remove(galeriKategorileri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaleriKategorileriExists(int id)
        {
          return (_context.GaleriKategorileri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kargo_Bitirme_Projesi.Entities;
using Kargo_Bitirme_Projesi.Identity;

namespace Kargo_Bitirme_Projesi.Controllers
{
    public class KargoIslemleriController : Controller
    {
        private readonly FerhatKahyaDbSetContext _context;

        public KargoIslemleriController(FerhatKahyaDbSetContext context)
        {
            _context = context;
        }

        // GET: KargoIslemleri
        public async Task<IActionResult> Index()
        {
            var ferhatKahyaDbSetContext = _context.KargoIslemleri.Include(k => k.Calisan).Include(k => k.Musteri);
            return View(await ferhatKahyaDbSetContext.ToListAsync());
        }

        // GET: KargoIslemleri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KargoIslemleri == null)
            {
                return NotFound();
            }

            var kargoIslemleri = await _context.KargoIslemleri
                .Include(k => k.Calisan)
                .Include(k => k.Musteri)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kargoIslemleri == null)
            {
                return NotFound();
            }

            return View(kargoIslemleri);
        }

        // GET: KargoIslemleri/Create
        public IActionResult Create()
        {
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "Id", "CalisanAdres");
            ViewData["MusteriId"] = new SelectList(_context.Musteriler, "Id", "MusteriAd");
            return View();
        }

        // POST: KargoIslemleri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TakipNo,KargoDesi,MusteriId,CalisanId,Price,KargoAlimTarihi,KargoTahminiTeslim,KargoTeslimTarihi,KargoDurum,TeslimAlan")] KargoIslemleri kargoIslemleri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kargoIslemleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "Id", "CalisanAdres", kargoIslemleri.CalisanId);
            ViewData["MusteriId"] = new SelectList(_context.Musteriler, "Id", "MusteriAd", kargoIslemleri.MusteriId);
            return View(kargoIslemleri);
        }

        // GET: KargoIslemleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KargoIslemleri == null)
            {
                return NotFound();
            }

            var kargoIslemleri = await _context.KargoIslemleri.FindAsync(id);
            if (kargoIslemleri == null)
            {
                return NotFound();
            }
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "Id", "CalisanAdres", kargoIslemleri.CalisanId);
            ViewData["MusteriId"] = new SelectList(_context.Musteriler, "Id", "MusteriAd", kargoIslemleri.MusteriId);
            return View(kargoIslemleri);
        }

        // POST: KargoIslemleri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TakipNo,KargoDesi,MusteriId,CalisanId,Price,KargoAlimTarihi,KargoTahminiTeslim,KargoTeslimTarihi,KargoDurum,TeslimAlan")] KargoIslemleri kargoIslemleri)
        {
            if (id != kargoIslemleri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kargoIslemleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KargoIslemleriExists(kargoIslemleri.Id))
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
            ViewData["CalisanId"] = new SelectList(_context.Calisanlar, "Id", "CalisanAdres", kargoIslemleri.CalisanId);
            ViewData["MusteriId"] = new SelectList(_context.Musteriler, "Id", "MusteriAd", kargoIslemleri.MusteriId);
            return View(kargoIslemleri);
        }

        // GET: KargoIslemleri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KargoIslemleri == null)
            {
                return NotFound();
            }

            var kargoIslemleri = await _context.KargoIslemleri
                .Include(k => k.Calisan)
                .Include(k => k.Musteri)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kargoIslemleri == null)
            {
                return NotFound();
            }

            return View(kargoIslemleri);
        }

        // POST: KargoIslemleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KargoIslemleri == null)
            {
                return Problem("Entity set 'FerhatKahyaDbSetContext.KargoIslemleri'  is null.");
            }
            var kargoIslemleri = await _context.KargoIslemleri.FindAsync(id);
            if (kargoIslemleri != null)
            {
                _context.KargoIslemleri.Remove(kargoIslemleri);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KargoIslemleriExists(int id)
        {
          return (_context.KargoIslemleri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

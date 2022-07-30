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
    public class CalisanlarController : Controller
    {
        private readonly FerhatKahyaDbSetContext _context;

        public CalisanlarController(FerhatKahyaDbSetContext context)
        {
            _context = context;
        }

        // GET: Calisanlar
        public async Task<IActionResult> Index()
        {
              return _context.Calisanlar != null ? 
                          View(await _context.Calisanlar.ToListAsync()) :
                          Problem("Entity set 'FerhatKahyaDbSetContext.Calisanlar'  is null.");
        }

        // GET: Calisanlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Calisanlar == null)
            {
                return NotFound();
            }

            var calisanlar = await _context.Calisanlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calisanlar == null)
            {
                return NotFound();
            }

            return View(calisanlar);
        }

        // GET: Calisanlar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calisanlar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CalisanAdi,CalisanSoyadi,CalisanTelefon,CalisanYas,CalisanAdres,CalisanResim")] Calisanlar calisanlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calisanlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calisanlar);
        }

        // GET: Calisanlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Calisanlar == null)
            {
                return NotFound();
            }

            var calisanlar = await _context.Calisanlar.FindAsync(id);
            if (calisanlar == null)
            {
                return NotFound();
            }
            return View(calisanlar);
        }

        // POST: Calisanlar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CalisanAdi,CalisanSoyadi,CalisanTelefon,CalisanYas,CalisanAdres,CalisanResim")] Calisanlar calisanlar)
        {
            if (id != calisanlar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calisanlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalisanlarExists(calisanlar.Id))
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
            return View(calisanlar);
        }

        // GET: Calisanlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Calisanlar == null)
            {
                return NotFound();
            }

            var calisanlar = await _context.Calisanlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calisanlar == null)
            {
                return NotFound();
            }

            return View(calisanlar);
        }

        // POST: Calisanlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Calisanlar == null)
            {
                return Problem("Entity set 'FerhatKahyaDbSetContext.Calisanlar'  is null.");
            }
            var calisanlar = await _context.Calisanlar.FindAsync(id);
            if (calisanlar != null)
            {
                _context.Calisanlar.Remove(calisanlar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalisanlarExists(int id)
        {
          return (_context.Calisanlar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

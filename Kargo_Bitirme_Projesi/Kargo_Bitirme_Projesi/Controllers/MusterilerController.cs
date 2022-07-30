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
    public class MusterilerController : Controller
    {
        private readonly FerhatKahyaDbSetContext _context;

        public MusterilerController(FerhatKahyaDbSetContext context)
        {
            _context = context;
        }

        // GET: Musteriler
        public async Task<IActionResult> Index()
        {
              return _context.Musteriler != null ? 
                          View(await _context.Musteriler.ToListAsync()) :
                          Problem("Entity set 'FerhatKahyaDbSetContext.Musteriler'  is null.");
        }

        // GET: Musteriler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Musteriler == null)
            {
                return NotFound();
            }

            var musteriler = await _context.Musteriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musteriler == null)
            {
                return NotFound();
            }

            return View(musteriler);
        }

        // GET: Musteriler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musteriler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MusteriAd,MusteriSoyad,MusteriTelefon,MusteriAdres")] Musteriler musteriler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musteriler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(musteriler);
        }

        // GET: Musteriler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Musteriler == null)
            {
                return NotFound();
            }

            var musteriler = await _context.Musteriler.FindAsync(id);
            if (musteriler == null)
            {
                return NotFound();
            }
            return View(musteriler);
        }

        // POST: Musteriler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MusteriAd,MusteriSoyad,MusteriTelefon,MusteriAdres")] Musteriler musteriler)
        {
            if (id != musteriler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musteriler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusterilerExists(musteriler.Id))
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
            return View(musteriler);
        }

        // GET: Musteriler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Musteriler == null)
            {
                return NotFound();
            }

            var musteriler = await _context.Musteriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musteriler == null)
            {
                return NotFound();
            }

            return View(musteriler);
        }

        // POST: Musteriler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Musteriler == null)
            {
                return Problem("Entity set 'FerhatKahyaDbSetContext.Musteriler'  is null.");
            }
            var musteriler = await _context.Musteriler.FindAsync(id);
            if (musteriler != null)
            {
                _context.Musteriler.Remove(musteriler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusterilerExists(int id)
        {
          return (_context.Musteriler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

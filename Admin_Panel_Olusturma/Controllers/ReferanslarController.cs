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
    public class ReferanslarController : Controller
    {
        private readonly FerhatKahyaDbContext _context;
        private readonly IWebHostEnvironment _host;
        public ReferanslarController(FerhatKahyaDbContext context,IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Referanslar
        public async Task<IActionResult> Index()
        {
            var ferhatKahyaDbContext = _context.Referanslar.Include(r => r.ReferansKategori);
            return View(await ferhatKahyaDbContext.ToListAsync());
        }

        // GET: Referanslar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Referanslar == null)
            {
                return NotFound();
            }

            var referanslar = await _context.Referanslar
                .Include(r => r.ReferansKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referanslar == null)
            {
                return NotFound();
            }

            return View(referanslar);
        }

        // GET: Referanslar/Create
        public IActionResult Create()
        {
            ViewData["ReferansId"] = new SelectList(_context.ReferansKategorileri, "Id", "ReferansAdi");
            return View();
        }

        // POST: Referanslar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReferansId,Durum,Baslik,UploadImage,KisaAciklama,Icerik")] Referanslar referanslar)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(referanslar.UploadImage.FileName);
                string extension = Path.GetExtension(referanslar.UploadImage.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath, "ReferansImage", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await referanslar.UploadImage.CopyToAsync(fileStream);
                }

                referanslar.Resim = fileName;

                _context.Add(referanslar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReferansId"] = new SelectList(_context.ReferansKategorileri, "Id", "ReferansAdi", referanslar.ReferansId);
            return View(referanslar);
        }

        // GET: Referanslar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Referanslar == null)
            {
                return NotFound();
            }

            var referanslar = await _context.Referanslar.FindAsync(id);
            if (referanslar == null)
            {
                return NotFound();
            }
            ViewData["ReferansId"] = new SelectList(_context.ReferansKategorileri, "Id", "ReferansAdi", referanslar.ReferansId);
            return View(referanslar);
        }

        // POST: Referanslar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReferansId,Durum,Baslik,UploadImage,KisaAciklama,Icerik")] Referanslar referanslar)
        {
            if (id != referanslar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(referanslar.UploadImage.FileName);
                    string extension = Path.GetExtension(referanslar.UploadImage.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "ReferansImage", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await referanslar.UploadImage.CopyToAsync(fileStream);
                    }

                    referanslar.Resim = fileName;
                    _context.Update(referanslar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferanslarExists(referanslar.Id))
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
            ViewData["ReferansId"] = new SelectList(_context.ReferansKategorileri, "Id", "ReferansAdi", referanslar.ReferansId);
            return View(referanslar);
        }

        // GET: Referanslar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Referanslar == null)
            {
                return NotFound();
            }

            var referanslar = await _context.Referanslar
                .Include(r => r.ReferansKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referanslar == null)
            {
                return NotFound();
            }

            return View(referanslar);
        }

        // POST: Referanslar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Referanslar == null)
            {
                return Problem("Entity set 'FerhatKahyaDbContext.Referanslar'  is null.");
            }
            var referanslar = await _context.Referanslar.FindAsync(id);
            if (referanslar != null)
            {
                var resimYolu = Path.Combine(_host.WebRootPath, "ReferansImage", referanslar.Resim);
                if (System.IO.File.Exists(resimYolu))//Bu belirlenen yolda bir dosya var mı?
                {
                    System.IO.File.Delete(resimYolu);//Buyoldaki dosyayı sil
                }

                _context.Referanslar.Remove(referanslar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferanslarExists(int id)
        {
          return (_context.Referanslar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

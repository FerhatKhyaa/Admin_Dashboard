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
    public class UrunKategorileriController : Controller
    {
        private readonly FerhatKahyaDbContext _context;
        private readonly IWebHostEnvironment _host;
        public UrunKategorileriController(FerhatKahyaDbContext context , IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: UrunKategorileri
        public async Task<IActionResult> Index()
        {
            var ferhatKahyaDbContext = _context.UrunKategorileri.Include(u => u.UrunKategori);
            return View(await ferhatKahyaDbContext.ToListAsync());
        }

        // GET: UrunKategorileri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UrunKategorileri == null)
            {
                return NotFound();
            }

            var urunKategorileri = await _context.UrunKategorileri
                .Include(u => u.UrunKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunKategorileri == null)
            {
                return NotFound();
            }

            return View(urunKategorileri);
        }

        // GET: UrunKategorileri/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.UrunKategorileri, "Id", "KategoriAdi");
            return View();
        }

        // POST: UrunKategorileri/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KategoriAdi,Durum,UploadImage,KategoriId")] UrunKategorileri urunKategorileri)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(urunKategorileri.UploadImage.FileName);
                string extension = Path.GetExtension(urunKategorileri.UploadImage.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath, "UrunImage", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await urunKategorileri.UploadImage.CopyToAsync(fileStream);
                }

                urunKategorileri.Resim = fileName;
                _context.Add(urunKategorileri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.UrunKategorileri, "Id", "KategoriAdi", urunKategorileri.KategoriId);
            return View(urunKategorileri);
        }

        // GET: UrunKategorileri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UrunKategorileri == null)
            {
                return NotFound();
            }

            var urunKategorileri = await _context.UrunKategorileri.FindAsync(id);
            if (urunKategorileri == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.UrunKategorileri, "Id", "KategoriAdi", urunKategorileri.KategoriId);
            return View(urunKategorileri);
        }

        // POST: UrunKategorileri/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KategoriAdi,Durum,UploadImage,KategoriId")] UrunKategorileri urunKategorileri)
        {
            if (id != urunKategorileri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(urunKategorileri.UploadImage.FileName);
                    string extension = Path.GetExtension(urunKategorileri.UploadImage.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "UrunImage", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await urunKategorileri.UploadImage.CopyToAsync(fileStream);
                    }

                    urunKategorileri.Resim = fileName;
                    _context.Update(urunKategorileri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunKategorileriExists(urunKategorileri.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.UrunKategorileri, "Id", "KategoriAdi", urunKategorileri.KategoriId);
            return View(urunKategorileri);
        }

        // GET: UrunKategorileri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UrunKategorileri == null)
            {
                return NotFound();
            }

            var urunKategorileri = await _context.UrunKategorileri
                .Include(u => u.UrunKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunKategorileri == null)
            {
                return NotFound();
            }

            return View(urunKategorileri);
        }

        // POST: UrunKategorileri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UrunKategorileri == null)
            {
                return Problem("Entity set 'FerhatKahyaDbContext.UrunKategorileri'  is null.");
            }
            var urunKategorileri = await _context.UrunKategorileri.FindAsync(id);
            if (urunKategorileri != null)
            {
                var resimYolu = Path.Combine(_host.WebRootPath, "UrunImage", urunKategorileri.Resim);
                if (System.IO.File.Exists(resimYolu))//Bu belirlenen yolda bir dosya var mı?
                {
                    System.IO.File.Delete(resimYolu);//Buyoldaki dosyayı sil
                }

                _context.UrunKategorileri.Remove(urunKategorileri);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunKategorileriExists(int id)
        {
          return (_context.UrunKategorileri?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

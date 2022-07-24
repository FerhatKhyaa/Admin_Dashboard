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
    public class UrunlerController : Controller
    {
        private readonly FerhatKahyaDbContext _context;
        private readonly IWebHostEnvironment _host;
        public UrunlerController(FerhatKahyaDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;   
        }

        // GET: Urunler
        public async Task<IActionResult> Index()
        {
            var ferhatKahyaDbContext = _context.Urunler.Include(u => u.UrunKategori);
            return View(await ferhatKahyaDbContext.ToListAsync());
        }

        // GET: Urunler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urunler = await _context.Urunler
                .Include(u => u.UrunKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunler == null)
            {
                return NotFound();
            }

            return View(urunler);
        }

        // GET: Urunler/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.UrunKategorileri, "Id", "KategoriAdi");
            return View();
        }

        // POST: Urunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KategoriId,Durum,Baslik,UploadImage,KisaAciklama,Icerik,Price")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(urunler.UploadImage.FileName);
                string extension = Path.GetExtension(urunler.UploadImage.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath, "UrunImage", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await urunler.UploadImage.CopyToAsync(fileStream);
                }

                urunler.Resim = fileName;

                _context.Add(urunler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.UrunKategorileri, "Id", "KategoriAdi", urunler.KategoriId);
            return View(urunler);
        }

        // GET: Urunler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urunler = await _context.Urunler.FindAsync(id);
            if (urunler == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.UrunKategorileri, "Id", "KategoriAdi", urunler.KategoriId);
            return View(urunler);
        }

        // POST: Urunler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KategoriId,Durum,Baslik,UploadImage,KisaAciklama,Icerik,Price")] Urunler urunler)
        {
            if (id != urunler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(urunler.UploadImage.FileName);
                    string extension = Path.GetExtension(urunler.UploadImage.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "UrunImage", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await urunler.UploadImage.CopyToAsync(fileStream);
                    }

                    urunler.Resim = fileName;
                    _context.Update(urunler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunlerExists(urunler.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.UrunKategorileri, "Id", "KategoriAdi", urunler.KategoriId);
            return View(urunler);
        }

        // GET: Urunler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urunler = await _context.Urunler
                .Include(u => u.UrunKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urunler == null)
            {
                return NotFound();
            }

            return View(urunler);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Urunler == null)
            {
                return Problem("Entity set 'FerhatKahyaDbContext.Urunler'  is null.");
            }
            var urunler = await _context.Urunler.FindAsync(id);
            if (urunler != null)
            {
                var resimYolu = Path.Combine(_host.WebRootPath, "UrunImage", urunler.Resim);
                if (System.IO.File.Exists(resimYolu))//Bu belirlenen yolda bir dosya var mı?
                {
                    System.IO.File.Delete(resimYolu);//Buyoldaki dosyayı sil
                }

                _context.Urunler.Remove(urunler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunlerExists(int id)
        {
          return (_context.Urunler?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

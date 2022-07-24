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
    public class ImageController : Controller
    {
        private readonly FerhatKahyaDbContext _context;
        private readonly IWebHostEnvironment _host;
        public ImageController(FerhatKahyaDbContext context,IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Image
        public async Task<IActionResult> Index()
        {
            var ferhatKahyaDbContext = _context.Image.Include(i => i.GaleriKategori);
            return View(await ferhatKahyaDbContext.ToListAsync());
        }

        // GET: Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.GaleriKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Image/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.GaleriKategorileri, "Id", "KategoriAdi");
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KategoriId,Durum,Baslik,UploadImage,KisaAciklama,Icerik")] Image image)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _host.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(image.UploadImage.FileName);
                string extension = Path.GetExtension(image.UploadImage.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath, "GaleriImage", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.UploadImage.CopyToAsync(fileStream);
                }

                image.Resim = fileName;
                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.GaleriKategorileri, "Id", "KategoriAdi", image.KategoriId);
            return View(image);
        }

        // GET: Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.GaleriKategorileri, "Id", "KategoriAdi", image.KategoriId);
            return View(image);
        }

        // POST: Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KategoriId,Durum,Baslik,UploadImage,KisaAciklama,Icerik")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _host.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(image.UploadImage.FileName);
                    string extension = Path.GetExtension(image.UploadImage.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "GaleriImage", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await image.UploadImage.CopyToAsync(fileStream);
                    }

                    image.Resim = fileName;
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.GaleriKategorileri, "Id", "KategoriAdi", image.KategoriId);
            return View(image);
        }

        // GET: Image/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.GaleriKategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Image == null)
            {
                return Problem("Entity set 'FerhatKahyaDbContext.Image'  is null.");
            }
            var image = await _context.Image.FindAsync(id);
            if (image != null)
            {
                var resimYolu = Path.Combine(_host.WebRootPath, "GaleriImage", image.Resim);
                if (System.IO.File.Exists(resimYolu))//Bu belirlenen yolda bir dosya var mı?
                {
                    System.IO.File.Delete(resimYolu);//Buyoldaki dosyayı sil
                }

                _context.Image.Remove(image);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return (_context.Image?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

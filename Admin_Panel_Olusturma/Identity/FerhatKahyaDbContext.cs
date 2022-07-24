using Admin_Panel_Olusturma.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admin_Panel_Olusturma.Identity
{
    public class FerhatKahyaDbContext : DbContext
    {
        public FerhatKahyaDbContext(DbContextOptions<FerhatKahyaDbContext>options) : base(options)
        {

        }
        public DbSet<Bloglar> Bloglar { get; set; }
        public DbSet<BlogKategorileri> BlogKategorileri { get; set; }
        public DbSet<ReferansKategorileri> ReferansKategorileri { get; set; }
        public DbSet<Referanslar> Referanslar { get; set; }
        public DbSet<GaleriKategorileri> GaleriKategorileri { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<UrunKategorileri> UrunKategorileri { get; set; }
        public DbSet<Urunler> Urunler { get; set; }
    }
}

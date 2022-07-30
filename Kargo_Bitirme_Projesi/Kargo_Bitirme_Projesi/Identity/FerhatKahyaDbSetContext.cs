using Kargo_Bitirme_Projesi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kargo_Bitirme_Projesi.Identity
{
    public class FerhatKahyaDbSetContext : IdentityDbContext<User, Role, string>
    {
        public FerhatKahyaDbSetContext(DbContextOptions<FerhatKahyaDbSetContext>options) : base(options)
        {

        }
        public DbSet<Musteriler> Musteriler { get; set; }
        public DbSet<KargoIslemleri> KargoIslemleri { get; set; }
        public DbSet<Calisanlar> Calisanlar { get; set; }
    }
}

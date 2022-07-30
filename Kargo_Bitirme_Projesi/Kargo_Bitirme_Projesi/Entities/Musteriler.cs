using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kargo_Bitirme_Projesi.Entities
{
    [Table("Musteriler")]
    public class Musteriler
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Müşteri Adı"),Required(ErrorMessage ="Müşteri Adı Boş Geçilemez!")]
        public string MusteriAd { get; set; }
        [Display(Name = "Müşteri Soyadı"),Required(ErrorMessage ="Müşteri Soyadı Boş Geçilemez!")]
        public string MusteriSoyad { get; set; }
        [Display(Name = "Müşteri Telefon"),Required(ErrorMessage ="Müşteri Telefon Boş Geçilemez!")]
        public string MusteriTelefon { get; set; }
        [Display(Name = "Müşteri Adres"),Required(ErrorMessage ="Müşteri Adresi Boş Geçilemez!")]
        public string MusteriAdres { get; set; }
        public virtual List<KargoIslemleri>? KargoIslemleri { get; set; }
    }
}

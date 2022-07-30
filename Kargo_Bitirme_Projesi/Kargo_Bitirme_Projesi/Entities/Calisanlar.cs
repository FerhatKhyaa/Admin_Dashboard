using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kargo_Bitirme_Projesi.Entities
{
    [Table("Calisanlar")]
    public class Calisanlar
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Çalışan Adı Boş Geçilemez!"), Display(Name ="Çalışan Adı")]
        public int CalisanAdi { get; set; }
        [Required(ErrorMessage = "Çalışan Soyadı Boş Geçilemez!"), Display(Name ="Çalışan Soyadı")]
        public int CalisanSoyadi { get; set; }
        [Required(ErrorMessage = "Çalışan Telefonu Boş Geçilemez!"), Display(Name ="Çalışan Telefon")]
        public string CalisanTelefon { get; set; }
        [Required(ErrorMessage = "Çalışan Yaş Boş Geçilemez!"), Display(Name ="Çalışan Yaş")]
        public int CalisanYas { get; set; }
        [Required(ErrorMessage = "Çalışan Adres Boş Geçilemez!"), Display(Name ="Çalışan Adres")]
        public string CalisanAdres { get; set; }
        [Required(ErrorMessage = "Çalışan Resim Boş Geçilemez!"), Display(Name ="Çalışan Resim")]
        public string CalisanResim { get; set; }
        public virtual List<KargoIslemleri>? KargoIslemleri { get; set; }

        [NotMapped]
        [Display(Name = "Resim Yükleyin...")]
        public IFormFile? UploadImage { get; set; }
    }
}

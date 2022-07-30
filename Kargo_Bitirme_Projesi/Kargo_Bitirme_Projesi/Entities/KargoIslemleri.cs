using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kargo_Bitirme_Projesi.Entities
{
    [Table("KargoIslemleri")]
    public class KargoIslemleri
    {
        [Key]
        public int Id { get; set; }
        [Required,Display(Name ="Takip Numarası")]
        public string TakipNo { get; set; }
        [Required(ErrorMessage ="Kargo Desi Boş Geçilemez!"),Display(Name ="Kargo Desi")]
        public decimal KargoDesi { get; set; }
        [Required(ErrorMessage ="Müşteri Boş Geçilemez!"), Display(Name = "Müşteri")]
        public int MusteriId { get; set; }
        [Required(ErrorMessage ="Çalışan Boş Geçilemez!"), Display(Name = "Çalışan")]
        public int CalisanId { get; set; }
        [Display(Name = "Ücret")]
        public decimal Price { get; set; }
        [Required, Display(Name = "Kargo Alım Tarihi")]
        public DateTime KargoAlimTarihi { get; set; }
        [Display(Name ="Kargo Tahmini Teslim")]
        public DateTime? KargoTahminiTeslim { get; set; }
        [Display(Name ="Kargo Teslim Tarihi")]
        public DateTime? KargoTeslimTarihi { get; set; }
        [Display(Name ="Kargo Durum"),Required(ErrorMessage ="Kargo Durumu Boş Geçilemez!")]
        public string KargoDurum { get; set; }
        public string? TeslimAlan { get; set; }

        [ForeignKey("CalisanId")]
        public virtual Calisanlar? Calisan { get; set; }
        [ForeignKey("MusteriId")]
        public virtual Musteriler? Musteri { get; set; }

    }
}

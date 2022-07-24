using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_Olusturma.Entities
{
    [Table("Referanslar")]
    public class Referanslar
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Referans Seçimi Boş Geçilemez"), Display(Name = "Referans Seçin")]
        public int ReferansId { get; set; }
        [Required(ErrorMessage = "Durum Boş Geçilemez"), Display(Name = "Durum Belirtin")]
        public int Durum { get; set; }
        [Required(ErrorMessage = "Başlık Boş Geçilemez"), Display(Name = "Başlık")]
        public string Baslik { get; set; }
        [Display(Name = "Resim")]
        public string? Resim { get; set; }
        [Display(Name = "Açıklama Alanı")]
        public string? KisaAciklama { get; set; }
        [Required(ErrorMessage = "İçerik Alanı Boş Geçilemez"), Display(Name = "İçerik Alanı")]
        public string Icerik { get; set; }
        [ForeignKey("ReferansId")]
        public virtual ReferansKategorileri? ReferansKategori { get; set; }

        [NotMapped]
        [Display(Name = "Resim Yükleyin...")]
        public IFormFile? UploadImage { get; set; }
    }
}

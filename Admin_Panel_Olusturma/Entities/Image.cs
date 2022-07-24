using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_Olusturma.Entities
{
    [Table("Image")]
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori Seçimi Boş Geçilemez"), Display(Name = "Kategori Seçin")]
        public int KategoriId { get; set; }
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
        [ForeignKey("KategoriId")]
        public virtual GaleriKategorileri? GaleriKategori { get; set; }

        [NotMapped]
        [Display(Name = "Resim Yükleyin...")]
        public IFormFile? UploadImage { get; set; }
    }
}

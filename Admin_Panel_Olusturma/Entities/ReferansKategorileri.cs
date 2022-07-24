using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_Olusturma.Entities
{
    [Table("ReferansKategorileri")]
    public class ReferansKategorileri
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori Adı Boş Geçilemez!"), Display(Name = "Kategori Adı Giriniz:")]
        public string ReferansAdi { get; set; }
        [Required(ErrorMessage = "Durum Boş Geçilemez!"), Display(Name = "Durum Belirtiniz:")]
        public int Durum { get; set; }
        [Display(Name = "Referans Kategori:")]
        public int? ReferansId { get; set; }
        [ForeignKey("ReferansId")]
        public virtual ReferansKategorileri? ReferansKategori { get; set; }
        public virtual List<Referanslar>? Referanslar { get; set; }
    }
}

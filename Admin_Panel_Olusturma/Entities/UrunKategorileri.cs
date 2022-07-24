﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin_Panel_Olusturma.Entities
{
    [Table("UrunKategorileri")]
    public class UrunKategorileri
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori Adı Boş Geçilemez!"), Display(Name = "Kategori Adı Giriniz:")]
        public string KategoriAdi { get; set; }
        [Required(ErrorMessage = "Durum Boş Geçilemez!"), Display(Name = "Durum Belirtiniz:")]
        public int Durum { get; set; }
        [Display(Name = "Resim")]
        public string? Resim { get; set; }
        [Display(Name = "Kategori Seçiniz:")]
        public int? KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public virtual UrunKategorileri? UrunKategori { get; set; }
        public virtual List<Urunler>? Urunler { get; set; }

        [NotMapped]
        [Display(Name = "Resim Yükleyin...")]
        public IFormFile? UploadImage { get; set; }
    }
}

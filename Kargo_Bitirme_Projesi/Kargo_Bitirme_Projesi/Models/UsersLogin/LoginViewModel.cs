using System.ComponentModel.DataAnnotations;

namespace Kargo_Bitirme_Projesi.Models.UsersLogin
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Kullanıcı Adınız")]
        [Required(ErrorMessage ="Kullanıcı Adı Alanı Boş Geçilemez!")]
        public string UserName { get; set; }
        [Display(Name = "Şifreniz")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Geçilemez!")] // Submit edilmesi için bu model'den geçiyor olması altındaki Field boş bırakılmadan submit edilemez.
        public string Password { get; set; }
    }
}

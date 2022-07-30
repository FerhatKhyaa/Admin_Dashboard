﻿using System.ComponentModel.DataAnnotations;

namespace Kargo_Bitirme_Projesi.Models.UsersLogin
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required]
        public string UserName { get; set; }


        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]//Password olarak Değer tipi gelir
        [Required] // Submit edilmesi için bu model'den geçiyor olması altındaki Field boş bırakılmadan submit edilemez.
        public string Password { get; set; }


        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        [Required] // Submit edilmesi için bu model'den geçiyor olması altındaki Field boş bırakılmadan submit edilemez.
        public string PasswordTekrar { get; set; }


        [Display(Name = "E-Posta Adresi")]
        [DataType(DataType.EmailAddress)]
        [Required] // Submit edilmesi için bu model'den geçiyor olması altındaki Field boş bırakılmadan submit edilemez.
        public string Email { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.CompanyModels
{
    public class Register
    {
        [Required]
        [Display(Name = "Ad Soyad")]
        public string NameSurname { get; set; }

        [Required]
        [Display(Name = "Eyalet/Bölge")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Yaşadığınız Şehir")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Yaşadığınız Ülke")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Firmadaki Göreviniz")]
        public string RespondTitle { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail Adresi")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Şifreniz 6 ila 40 karakter aralığında olmalıdır.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Şifre Tekrar")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Şifreniz 6 ila 40 karakter aralığında olmalıdır.")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public Register()
        {
            CreatedDate = DateTime.Now.ToLocalTime();
        }
    }
}

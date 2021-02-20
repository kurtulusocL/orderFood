using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.AdminModels
{
    public class ChangeProfile
    {
        [Required]
        [Display(Name = "Ad Soyad")]
        public string NameSurname { get; set; }        

        [Required]
        [Display(Name = "Doğum Tarihiniz")]
        public DateTime Birthdate { get; set; }

        [Required]
        [Display(Name = "Görevi")]
        public string RespondTitle { get; set; }        

        [Required]
        [Display(Name = "Yaşadığınız Ülke")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Yaşadığınız Şehir")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Eyalet/Bölge")]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Telefon No")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public DateTime? UpdatedDate { get; set; }
        public bool IsConfirm { get; set; }
        public ChangeProfile()
        {
            IsConfirm = true;
        }
    }
}

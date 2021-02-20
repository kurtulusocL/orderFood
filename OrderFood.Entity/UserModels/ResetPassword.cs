using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.UserModels
{
    public class ResetPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Code { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Şifreniz 6 ila 40 karakter aralığında olmalıdır.")]
        public string Password { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Şifreniz 6 ila 40 karakter aralığında olmalıdır.")]
        public string ConfirmPassword { get; set; }
    }
}

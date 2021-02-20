using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class CompanyContact : BaseHome
    {
        [Required]
        [EmailAddress]
        public string EMail { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Url]
        public string Location { get; set; }
        [Required]
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }

        public string UserId { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public CompanyContact()
        {
            IsConfirmed = true;
        }
    }
}

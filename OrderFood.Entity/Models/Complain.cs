using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Complain : BaseHome
    {
        [Required]
        public string NameSurname { get; set; }
        [Required]
        [EmailAddress]
        public string EMail { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Text { get; set; }

        public int? ProductId { get; set; }
        public int? CompanyId { get; set; }
        public string UserId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Company Company { get; set; }

        public Complain()
        {
            IsConfirmed = false;
        }
    }
}

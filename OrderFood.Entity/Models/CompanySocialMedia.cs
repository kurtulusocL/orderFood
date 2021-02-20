using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class CompanySocialMedia:BaseHome
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
        public string Logo { get; set; }

        public string UserId { get; set; }

        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public CompanySocialMedia()
        {
            IsConfirmed = true;
        }
    }
}

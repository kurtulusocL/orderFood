using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class SocialMedia : BaseHome
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
        public string Photo { get; set; }
        public SocialMedia()
        {
            IsConfirmed = true;
        }
    }
}

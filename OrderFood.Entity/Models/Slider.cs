using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Slider : BaseHome
    {
        public string Title { get; set; }
        [Required]
        public string Photo { get; set; }
        public Slider()
        {
            IsConfirmed = true;
        }
    }
}

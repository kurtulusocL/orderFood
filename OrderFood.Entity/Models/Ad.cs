using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Ad : BaseHome
    {
        public string Title { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        [Url]
        public string Website { get; set; }
        public int? Hit { get; set; }
        [Required]
        public string Photo { get; set; }
        [Required]
        public DateTime DeletedTime { get; set; }
        public Ad()
        {
            Hit = 0;
            IsConfirmed = true;
        }
    }
}

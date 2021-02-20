using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class VideoAd : BaseHome
    {
        public string Title { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Website { get; set; }
        [Required]
        public string VideoLink { get; set; }
        public int? Hit { get; set; }
        [Required]
        public DateTime DeletedTime { get; set; }
        public VideoAd()
        {
            Hit = 0;
            IsConfirmed = true;
        }
    }
}

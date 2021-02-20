using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Contact : BaseHome
    {
        [Required]
        public string Address { get; set; }
        [Required]
        [Url]
        public string Location { get; set; }
        [Required]
        [EmailAddress]
        public string EMail { get; set; }
        public string FaxNumber { get; set; }
        public int? Hit { get; set; }
        public Contact()
        {
            IsConfirmed = true;
            Hit = 0;
        }
    }
}

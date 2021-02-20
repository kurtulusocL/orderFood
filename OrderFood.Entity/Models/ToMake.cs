using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class ToMake : BaseHome
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public string NameSurname { get; set; }
        [Required]
        public DateTime TillWhen { get; set; }
        public bool IsFinished { get; set; }

        public string UserId { get; set; }
        public ToMake()
        {
            IsConfirmed = true;
            IsFinished = false;
        }
    }
}

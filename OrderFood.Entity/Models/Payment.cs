using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Payment : BaseHome
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photo { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}

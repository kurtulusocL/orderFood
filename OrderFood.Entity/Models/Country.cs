using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Country : BaseHome
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photo { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}

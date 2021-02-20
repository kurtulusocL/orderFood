using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class City : BaseHome
    {
        [Required]
        public string Name { get; set; }

        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

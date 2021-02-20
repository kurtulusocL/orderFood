using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Category : BaseHome
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Photo { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}

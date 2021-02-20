using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class ProductDetail : BaseHome
    {
        public string Warning { get; set; }
        public string Detail { get; set; }
        public string Articles { get; set; }
        public string Offers { get; set; }
        [Required]
        public string ShopServiceTime { get; set; }
        [Required]
        public string OrderTime { get; set; }
        public string UserId { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Product : BaseHome
    {
        [Required]
        public string ProductNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
        public int? Hit { get; set; }
        [Required]
        public decimal SellingPrice { get; set; }
        public decimal? DicsountPrice { get; set; }
        public int? Like { get; set; }

        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public string UserId { get; set; }
        public int ProductDetailId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int? CompanyId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }
        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Company Company { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }

        public Product()
        {
            Hit = 0;
            Like = 0;
            DicsountPrice = 0;
        }
    }
}

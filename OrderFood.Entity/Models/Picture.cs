using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Picture : BaseHome
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; }

        public int? ProductId { get; set; }
        public string UserId { get; set; }
        public int? CompanyId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Company Company { get; set; }
    }
}

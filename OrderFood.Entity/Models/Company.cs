using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Company : BaseHome
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Detail { get; set; }
        public string Subdetail { get; set; }
        public int? Hit { get; set; }
        public int? Like { get; set; }
        [Required]
        public string Logo { get; set; }

        public string UserId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int KindId { get; set; }

        public virtual Country Country { get; set; }
        public virtual City City { get; set; }
        public virtual Kind Kind { get; set; }

        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
        public virtual ICollection<CompanySocialMedia> CompanySocialMedias { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CancelRequest> CancelRequests { get; set; }

        public Company()
        {
            Hit = 0;
            Like = 0;
            IsConfirmed = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class CancelRequest : BaseHome
    {
        [Required]
        public string NameSurname { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public DateTime When { get; set; }

        public string UserId { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public CancelRequest()
        {
            IsConfirmed = true;
        }
    }
}

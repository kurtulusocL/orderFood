using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Entity.Models
{
    public class Order : BaseHome
    {
        [Required]
        public string NameSurname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public int Piece { get; set; }
        public decimal SellingPrice { get; set; }
        public double? Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public bool InBox { get; set; }
        public int? Hit { get; set; }
        public bool IsSent { get; set; }    
        
        public string UserId { get; set; }
        public int PaymentId { get; set; }
        public int? ProductId { get; set; }
        public int? CompanyId { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Product Product { get; set; }
        public virtual Company Company { get; set; }
        public Order()
        {
            InBox = true;
            Hit = 0;
            Discount = 0;
            IsSent = false;
        }
    }
}
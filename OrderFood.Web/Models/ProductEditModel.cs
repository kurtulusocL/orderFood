using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderFood.Web.Models
{
    public class ProductEditModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
    }
    public class ProductEditDiscount
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
    }
}
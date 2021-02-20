using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ProductDetailService
{
    public interface IProductDetailService
    {        
        List<ProductDetail> GetAllIncluding();
        ProductDetail GetById(int? id);
        void Delete(ProductDetail model);        
        void SetActive(int id);
        void SetDeActive(int id);
    }
}

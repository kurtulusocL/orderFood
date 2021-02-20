using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ProductService
{
    public interface IProductService
    {
        void Create(Product model);
        void Update(Product model);
        List<Product> GetAllIncluding();
        Product GetById(int? id);
        void Delete(Product model);
        Product HitRead(int? id);
        Product Like(int? id);
        void SetActive(int id);
        void SetDeActive(int id);
    }
}

using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void Create(Product model)
        {
            _productRepository.Create(model);
        }

        public void Delete(Product model)
        {
            _productRepository.Delete(model);
        }

        public List<Product> GetAllIncluding()
        {
            return _productRepository.GetAllIncluding();
        }

        public Product GetById(int? id)
        {
            return _productRepository.GetById(id);
        }

        public Product HitRead(int? id)
        {
            return _productRepository.HitRead(id);
        }

        public Product Like(int? id)
        {
            return _productRepository.Like(id);
        }

        public void SetActive(int id)
        {
            _productRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _productRepository.SetDeActive(id);
        }

        public void Update(Product model)
        {
            _productRepository.Update(model);
        }
    }
}

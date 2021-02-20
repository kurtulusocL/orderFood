using OrderFood.Business.Interfaces;
using OrderFood.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderFood.Service.ProductDetailService
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IProductDetailRepository _productDetailRepository;
        public ProductDetailService(IProductDetailRepository productDetailRepository)
        {
            _productDetailRepository = productDetailRepository;
        }
        public void Delete(ProductDetail model)
        {
            _productDetailRepository.Delete(model);
        }

        public List<ProductDetail> GetAllIncluding()
        {
            return _productDetailRepository.GetAllIncluding();
        }

        public ProductDetail GetById(int? id)
        {
            return _productDetailRepository.GetById(id);
        }

        public void SetActive(int id)
        {
            _productDetailRepository.SetActive(id);
        }

        public void SetDeActive(int id)
        {
            _productDetailRepository.SetDeActive(id);
        }
    }
}

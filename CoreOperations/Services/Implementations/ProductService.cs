using CoreOperations.Models;
using CoreOperations.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CoreOperations.Services.Implementations
{
    public class ProductService : Service, IProductService
    {
        public ProductService()
        {

        }

        public List<Product> GetAllProducts()
        {
            var productRepository = UnitOfWork.GetRepository<Product>();
            var products = productRepository.GetAll().ToList();
            return products;
        }

        public Product GetProductsById(int productId)
        {
            var product = UnitOfWork.GetRepository<Product>().GetAll()
                .FirstOrDefault(item => item.ProductId.Equals(productId));
            return product;
        }
    }
}

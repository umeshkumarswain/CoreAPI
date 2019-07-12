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

        public void UpdateProduct(Product prod)
        {
            try
            {
                var product = UnitOfWork.GetRepository<Product>().GetAll()
                                 .FirstOrDefault(item => item.ProductId.Equals(prod.ProductId));
                var entity = new Product
                {
                    Name = prod.Name,
                    Color = prod.Color,
                    DiscontinuedDate = prod.DiscontinuedDate,
                    ListPrice = prod.ListPrice,
                    ModifiedDate = prod.ModifiedDate,
                    ProductCategory = prod.ProductCategory,
                    ProductModel = prod.ProductModel,
                    ProductModelId = prod.ProductModelId,
                    SalesOrderDetail = prod.SalesOrderDetail,
                    SellEndDate = prod.SellEndDate,
                    SellStartDate = prod.SellStartDate,
                    ProductNumber = prod.ProductNumber,
                    StandardCost = prod.StandardCost,
                    Size = prod.Size,
                    Weight = prod.Weight,
                    ThumbNailPhoto = prod.ThumbNailPhoto,
                    ThumbnailPhotoFileName = prod.ThumbnailPhotoFileName
                };
                UnitOfWork.GetRepository<Product>().Edit(entity);
                UnitOfWork.Save();
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        public void DeleteeProduct(Product prod)
        {
            try
            {
                var product = UnitOfWork.GetRepository<Product>().GetAll()
                                .FirstOrDefault(item => item.ProductId.Equals(prod.ProductId));
                UnitOfWork.GetRepository<Product>().Remove(product);
                UnitOfWork.Save();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}

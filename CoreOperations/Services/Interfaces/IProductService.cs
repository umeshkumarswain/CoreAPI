using CoreOperations.Models;
using System.Collections.Generic;

namespace CoreOperations.Services.Interfaces
{

    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetProductsById(int productId);
    }

}

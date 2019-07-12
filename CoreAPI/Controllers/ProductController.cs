using CoreOperations.Models;
using CoreOperations.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _service;
        public ProductController(IProductService _productService)
        {
            _service = _productService;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            var products = _service.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _service.GetProductsById(id);
            return product != null ? (ActionResult<Product>)Ok(product) : NotFound();
        }
        [HttpPut]
        public ActionResult<Product> Update(Product product)
        {
            _service.UpdateProduct(product);
            return Ok("Success");
        }
        [HttpDelete]
        public ActionResult<Product> Delete(Product product)
        {
            _service.DeleteeProduct(product);
            return Ok("Success");
        }
    }
}

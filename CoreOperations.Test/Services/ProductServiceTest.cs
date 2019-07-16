using CoreOperations.Models;
using CoreOperations.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreOperations.Test.Services
{
    [TestClass]
    public class ProductServiceTest
    {

        Mock<IProductService> mockProductService = new Mock<IProductService>();
        [TestMethod]
        public void GetAll()
        {
            var products = new List<Product>() {
                new Product{ Name="Product One",Color="Green",ListPrice=12}
            };
            var productservice = new Mock<IProductService>();
            var result = productservice.Setup( item => item.GetAllProducts())
                           .Returns(products);
            Assert.AreEqual(products, result);


        }
    }
}

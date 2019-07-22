using CoreOperations.Models;
using CoreOperations.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _service;
        public CustomerController(ICustomerService _customerService)
        {
            _service = _customerService;
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAll()
        {
            var customers = _service.GetAllCustomer();
            return Ok(customers);
        }
        [HttpPost("{id}")]
        public ActionResult<Customer> Get(int id)
        {
            var customer = _service.GetCustomerById(id);
            return customer != null ? (ActionResult<Customer>)Ok(customer) : NotFound();
        }
        [HttpPut]
        public ActionResult<Product> Update(Customer customer)
        {
            _service.UpdateCustomer(customer);
            return Ok("Success");
        }
        [HttpDelete]
        public ActionResult<Product> Delete(Customer customer)
        {
            _service.DeleteCustomer(customer);
            return Ok("Success");
        }
    }
}

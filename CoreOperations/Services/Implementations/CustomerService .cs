using CoreOperations.Models;
using CoreOperations.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CoreOperations.Services.Implementations
{
    public class CustomerService : Service, ICustomerService
    {
        public CustomerService()
        {

        }
        [HttpGet]
        public List<Customer> GetAllCustomer()
        {
            var customerRepository = UnitOfWork.GetRepository<Customer>();
            var customers = customerRepository.GetAll().ToList();
            return customers;
        }
        [HttpPost]
        public Customer GetCustomerById(int custId)
        {
            var product = UnitOfWork.GetRepository<Customer>().GetAll()
                          .FirstOrDefault(item => item.CustomerId.Equals(custId));
            return product;
        }
        [HttpPut]
        public void UpdateCustomer(Customer cust)
        {
            try
            {
                var product = UnitOfWork.GetRepository<Customer>().GetAll()
                                 .FirstOrDefault(item => item.CustomerId.Equals(cust.CustomerId));
                var entity = new Customer()
                {
                    CompanyName = cust.CompanyName,
                    EmailAddress = cust.EmailAddress,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Phone = cust.Phone
                };
                UnitOfWork.GetRepository<Customer>().Edit(entity);
                UnitOfWork.Save();
            }
            catch (System.Exception)
            {
                throw;
            }

        }
        [HttpDelete]
        public void DeleteCustomer(Customer customer)
        {
            try
            {
                var customerdetails = UnitOfWork.GetRepository<Customer>().GetAll()
                                .FirstOrDefault(item => item.CustomerId.Equals(customer.CustomerId));
                UnitOfWork.GetRepository<Customer>().Remove(customerdetails);
                UnitOfWork.Save();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}

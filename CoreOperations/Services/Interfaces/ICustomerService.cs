using System.Collections.Generic;
using CoreOperations.Models;

namespace CoreOperations.Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomer();
        Customer GetCustomerById(int productId);
        void UpdateCustomer(Customer product);
        void DeleteCustomer(Customer product);
    }
}
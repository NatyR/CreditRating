using System.Collections.Generic;
using System.Threading.Tasks;
using Customer.API.Entities;

namespace Customer.API.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomer(CustomerData customer);
        Task<bool> ProcessMessage(string origin, string message);

    }
}
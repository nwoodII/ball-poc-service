    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ball.Poc.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Ball.Poc.Service.Repository
{
    public interface ICosmosDBService
    {
        Task<IEnumerable<CoreDBCustomer>> GetCustomersAsync(string queryString);
        Task<CoreDBCustomer> GetCustomerAsync(string id);
        Task<CoreDBCustomer> AddCustomerAsync(CoreDBCustomer dbCustomer);
        Task<CoreDBCustomer> UpdateCustomerAsync(string id, CoreDBCustomer dbCustomer);
        Task DeleteCustomerAsync(string id);
    }
}
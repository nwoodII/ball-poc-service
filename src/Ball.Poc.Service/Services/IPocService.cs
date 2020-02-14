using Ball.Poc.Service.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ball.Poc.Service.Services {
    public interface IPocService<T>{
        Task<IEnumerable<CustomerDto>> getCustomers(string query);
        Task<CustomerDto> getCustomer(string id);
        Task<CustomerDto> saveCustomer(CustomerDto customerDto);
        Task<CustomerDto> updateCustomer(CustomerDto customerDTO);
        Task deleteCustomer(string id);
    }
}
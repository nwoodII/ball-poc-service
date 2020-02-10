using Ball.Poc.Service.DTOs;

namespace Ball.Poc.Service.Services {
    public interface IPocService<T>
    {
        CustomerDto save(CustomerDto customerDto);
    }
}
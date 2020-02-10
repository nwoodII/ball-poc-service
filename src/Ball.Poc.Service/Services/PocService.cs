using Ball.Poc.Service.DTOs;

namespace Ball.Poc.Service.Services {
    public class PocService : IPocService<PocService> {

        public CustomerDto save(CustomerDto customerDTO) {

            var dto = new CustomerDto();
            {
                dto = customerDTO;
                dto.Id = 20;
            };
            return dto;
        }

    }
}
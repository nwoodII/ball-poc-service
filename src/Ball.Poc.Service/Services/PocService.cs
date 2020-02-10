using System;
using Ball.Poc.Service.DTOs;
using Microsoft.Extensions.Logging;

namespace Ball.Poc.Service.Services {
    public class PocService : IPocService<PocService> {

        private readonly ILogger<PocService> _logger;
        public PocService(ILogger<PocService> logger) {
            _logger = logger;
        }
        public CustomerDto save(CustomerDto customerDTO) {
            _logger.LogInformation("Inside save(CustomerDto customerDTO)");
            var dto = new CustomerDto();
            {
                dto = customerDTO;
                dto.Id = Guid.NewGuid();
            };
            _logger.LogInformation("Leaving save(CustomerDto customerDTO)", dto);
            return dto;
        }

    }
}
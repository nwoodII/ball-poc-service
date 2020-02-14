using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ball.Poc.Service.DTOs;
using Ball.Poc.Service.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Ball.Poc.Service.Controllers
{

    [ApiController]
    [Route("api/")]
    public class PocController : ControllerBase
    {
        private readonly ILogger<PocController> _logger;
        private readonly IPocService<PocService> _pocService;

        public PocController(ILogger<PocController> logger, IPocService<PocService> pocService)
        {
            _logger = logger;
            _pocService = pocService;
        }

        [HttpGet("poc")]
        public async Task<IEnumerable<CustomerDto>> GetCustomers()
        {
            _logger.LogInformation("Inside GetCustomers()");
            IEnumerable<CustomerDto> results = await _pocService.getCustomers("Select * from Customers");
            _logger.LogInformation("Leaving GetCustomers()");
            return results;
        }

        [HttpGet("poc/{id:guid}", Name = nameof(GetCustomer))]
        public async Task<ActionResult<CustomerDto>> GetCustomer(Guid id)
        {
            _logger.LogInformation("Inside GetCustomer(int id)");
            CustomerDto dto = await _pocService.getCustomer(id.ToString());
            _logger.LogInformation("Leaving GetCustomer(int id)");
            return dto;
        }

        [HttpPost("poc")]
        [ProducesResponseType(201, Type = typeof(CustomerDto))]        
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerDto createCustomerDTO)
        {
            _logger.LogInformation("Inside CreateCustomer()");
            CustomerDto customer = await _pocService.saveCustomer(createCustomerDTO);
            return CreatedAtAction(nameof(GetCustomer),new { id = customer.Id}, customer);
        }

        [HttpPut("poc/{id:guid}")]
        public async Task<ActionResult<CustomerDto>> UpdateCustomer(Guid id, CustomerDto customerDto){
            _logger.LogInformation("Inside UpdateCustomer(CustomerDto updateCustomerDTO)");
            customerDto.Id = id;
            CustomerDto updatedCustomer = await _pocService.updateCustomer(customerDto);
            return CreatedAtAction(nameof(GetCustomer), new {id = updatedCustomer.Id}, updatedCustomer);
        }

        [HttpDelete("poc/{id:guid}")]
        public async Task<ActionResult> DeleteCustomer(Guid id){
            _logger.LogInformation("Inside DeleteCustomer(Guid id)");
            await _pocService.deleteCustomer(id.ToString());
            _logger.LogInformation("Leaving DeleteCustomer(Guid id)");
            return NoContent();
        }

    }
}
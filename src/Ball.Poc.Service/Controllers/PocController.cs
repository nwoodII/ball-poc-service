using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ball.Poc.Service.DTOs;
using Ball.Poc.Service.Services;
using System;

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

        [HttpGet("poc/{id:guid}", Name = nameof(GetCustomer))]
        public ActionResult<CustomerDto> GetCustomer(Guid id)
        {
            _logger.LogInformation("Inside GetCustomer(int id)");
            var dto = new CustomerDto();
            dto.Id = Guid.NewGuid();
            dto.FirstName = "Nathan";
            dto.LastName = "Woodson";
            dto.Address.Line1 = "320 Ren Road";
            dto.Address.City = "Richmond";
            dto.Address.State = "VA";
            dto.Address.Zip = "23231";
            _logger.LogInformation("Leaving GetCustomer(int id)");
            return dto;
        }

        [HttpPost("poc")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]        
        public ActionResult<CustomerDto> CreateCustomer(CustomerDto creatCustomerDTO)
        {
            _logger.LogInformation("Inside CreateCustomer()");
            var dto = _pocService.save(creatCustomerDTO);
            return CreatedAtAction(nameof(GetCustomer),new { id = dto.Id}, dto);
        }
    }
}
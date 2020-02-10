using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ball.Poc.Service.DTOs;
using Ball.Poc.Service.Services;

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

        [HttpGet("poc/{id:int}", Name = nameof(GetCustomer))]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var dto = new CustomerDto();
            dto.Id = id;
            dto.Address = "test";
            return dto;
        }

        [HttpPost("poc")]
        [ProducesResponseType(200, Type = typeof(CustomerDto))]        
        public ActionResult<CustomerDto> CreateCustomer(CustomerDto creatCustomerDTO)
        {
            var dto = _pocService.save(creatCustomerDTO);
            //CreatedAtAction()
            return CreatedAtAction(nameof(GetCustomer),new { id = creatCustomerDTO.Id}, dto);
        }
    }
}
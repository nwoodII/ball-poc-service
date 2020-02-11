using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ball.Poc.Service.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressDto Address { get; set; }
    }
}
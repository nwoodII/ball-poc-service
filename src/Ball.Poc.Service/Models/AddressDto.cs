using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ball.Poc.Repository.Models;

namespace Ball.Poc.Service.DTOs
{
    public class AddressDto
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
        public string Zip { get; set; }

        public static implicit operator AddressDto(Address v)
        {
            throw new NotImplementedException();
        }
    }
}
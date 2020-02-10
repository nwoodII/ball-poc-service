using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ball.Poc.Service.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
        public string Zip { get; set; }
    }
}
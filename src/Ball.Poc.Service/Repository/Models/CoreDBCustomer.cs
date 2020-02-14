using Newtonsoft.Json;

namespace Ball.Poc.Repository.Models {

    public class CoreDBCustomer {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
        public string Zip { get; set; }
    }

}
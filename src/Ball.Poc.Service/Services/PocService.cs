using System;
using Ball.Poc.Service.DTOs;
using Ball.Poc.Repository.Models;
using Ball.Poc.Service.Repository;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace Ball.Poc.Service.Services {
    public class PocService : IPocService<PocService> {
        private readonly ILogger<PocService> _logger;
        private readonly ICosmosDBService _cosmosDB;
        public PocService(ILogger<PocService> logger, ICosmosDBService cosmosDB) {
            _logger = logger;
            _cosmosDB = cosmosDB;
        }
        public async Task<IEnumerable<CustomerDto>> getCustomers(string query){
            _logger.LogInformation("Inside getCustomers()");
            List<CustomerDto> results = new List<CustomerDto>();

            await _cosmosDB.GetCustomersAsync(query);

            var dbCustomers = await _cosmosDB.GetCustomersAsync(query);
            foreach (var dbCustomer in dbCustomers)
            {
                CustomerDto dto = new CustomerDto
                {
                   Id = Guid.Parse(dbCustomer.Id),
                    FirstName = dbCustomer.FirstName,
                    LastName = dbCustomer.LastName,
                    Address = new AddressDto {
                        Line1 = dbCustomer.Address.Line1,
                        Line2 = dbCustomer.Address.Line2,
                        City = dbCustomer.Address.City,
                        State = dbCustomer.Address.State,
                        Zip = dbCustomer.Address.Zip,
                    } 
                };
                results.Add(dto);
            }
            return results;
        }
        public async Task<CustomerDto> getCustomer(string id) {

            _logger.LogInformation("Inside getCustomer(string id)");
            CoreDBCustomer dbCustomer = await _cosmosDB.GetCustomerAsync(id);

            CustomerDto dto = new CustomerDto
            {
                Id = Guid.Parse(dbCustomer.Id),
                FirstName = dbCustomer.FirstName,
                LastName = dbCustomer.LastName,
                Address = new AddressDto {
                    Line1 = dbCustomer.Address.Line1,
                    Line2 = dbCustomer.Address.Line2,
                    City = dbCustomer.Address.City,
                    State = dbCustomer.Address.State,
                    Zip = dbCustomer.Address.Zip,
                }
            };
            _logger.LogInformation("Leaving getCustomer(string id)", dto);
            return dto;
        }

        public async Task<CustomerDto> saveCustomer(CustomerDto customerDTO) {
            _logger.LogInformation("Inside saveCustomer(CustomerDto customerDTO)");
            CoreDBCustomer dBCustomer = new CoreDBCustomer
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Address = new Address{
                    Line1 = customerDTO.Address.Line1,
                    Line2 = customerDTO.Address.Line2,
                    City = customerDTO.Address.City,
                    State = customerDTO.Address.State,
                    Zip = customerDTO.Address.Zip,
                }
            };

            CoreDBCustomer result = await _cosmosDB.AddCustomerAsync(dBCustomer);
            CustomerDto dto = new CustomerDto
            {
                Id = Guid.Parse(result.Id),
                FirstName = result.FirstName,
                LastName = result.LastName,
                Address = new AddressDto {
                    Line1 = result.Address.Line1,
                    Line2 = result.Address.Line2,
                    City = result.Address.City,
                    State = result.Address.State,
                    Zip = result.Address.Zip
                }
            };

            _logger.LogInformation("Leaving saveCustomer(CustomerDto customerDTO)", dto);
            return dto;
        }

        public async Task<CustomerDto> updateCustomer(CustomerDto customerDTO) {
            _logger.LogInformation("Inside updateCustomer(CustomerDto customerDTO)");
            CoreDBCustomer dBCustomer = new CoreDBCustomer
            {
                Id = customerDTO.Id.ToString(),
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Address = new Address{
                    Line1 = customerDTO.Address.Line1,
                    Line2 = customerDTO.Address.Line2,
                    City = customerDTO.Address.City,
                    State = customerDTO.Address.State,
                    Zip = customerDTO.Address.Zip,
                }
            };
            _logger.LogInformation(dBCustomer.Id);
            CoreDBCustomer result = await _cosmosDB.UpdateCustomerAsync(dBCustomer.Id, dBCustomer);
            CustomerDto dto = new CustomerDto
            {
                Id = Guid.Parse(result.Id),
                FirstName = result.FirstName,
                LastName = result.LastName,
                Address = new AddressDto {
                    Line1 = result.Address.Line1,
                    Line2 = result.Address.Line2,
                    City = result.Address.City,
                    State = result.Address.State,
                    Zip = result.Address.Zip
                }
            };

            _logger.LogInformation("Leaving updateCustomer(CustomerDto customerDTO)", dto);
            return dto;
        }

        public async Task deleteCustomer(string id) {
            await _cosmosDB.DeleteCustomerAsync(id);
        }
    }
}
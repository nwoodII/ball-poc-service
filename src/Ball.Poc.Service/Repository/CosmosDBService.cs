    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ball.Poc.Service.Repository;
    using Ball.Poc.Repository.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;
    using System.Net;
    using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Ball.Poc.Service.Repository
{

    public class CosmosDBService : ICosmosDBService
    {
        private Container _container;

        public CosmosDBService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
        public async Task<CoreDBCustomer> GetCustomerAsync(string id)
        {
            try
            {
                ItemResponse<CoreDBCustomer> response = await this._container.ReadItemAsync<CoreDBCustomer>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            { 
                return null;
            }
        }

        public async Task<IEnumerable<CoreDBCustomer>> GetCustomersAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<CoreDBCustomer>(new QueryDefinition(queryString));
            List<CoreDBCustomer> results = new List<CoreDBCustomer>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                
                results.AddRange(response.ToList());
            }

            return results;
        }


        public async Task<CoreDBCustomer> AddCustomerAsync(CoreDBCustomer dBCustomer)
        {
            ItemResponse<CoreDBCustomer> response = await this._container.CreateItemAsync<CoreDBCustomer>(dBCustomer, new PartitionKey(dBCustomer.Id));
            return response;
        }

        public async Task<CoreDBCustomer> UpdateCustomerAsync(string id, CoreDBCustomer dBCustomer)
        {
            ItemResponse<CoreDBCustomer> response = await this._container.UpsertItemAsync<CoreDBCustomer>(dBCustomer, new PartitionKey(id));
            return response;
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await this._container.DeleteItemAsync<CoreDBCustomer>(id, new PartitionKey(id));
        }
    }
}
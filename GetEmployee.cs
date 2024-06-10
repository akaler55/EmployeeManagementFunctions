using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;

namespace EmployeeManagementFunctions
{
    public static class GetEmployee
    {
        private static readonly string cosmosConnectionString = Environment.GetEnvironmentVariable("CosmosDBConnectionString");
        private static readonly string databaseName = Environment.GetEnvironmentVariable("CosmosDBDatabaseName");
        private static readonly string containerName = Environment.GetEnvironmentVariable("CosmosDBContainerName");


        [FunctionName("GetEmployee")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "employee/{id}")] HttpRequest req,
            String id,
            ILogger log)
        {
            try
            {
                // Initialize Cosmos DB client
                CosmosClient client = new(cosmosConnectionString);
                Database database = client.GetDatabase(databaseName);
                Container container = database.GetContainer(containerName);

                // Retrieve employee document by id
                ItemResponse<Employee> response = await container.ReadItemAsync<Employee>(id, new PartitionKey(id));
                
                // Read existing item from container
                Employee employee = response.Resource;
                return new OkObjectResult(employee);
            }
            catch (Exception ex)
            {
                log.LogError($"Error retrieving employee: {ex.Message}");
                return new NotFoundResult();
            }
        }
    }
}

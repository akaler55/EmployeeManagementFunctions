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
    
    public static class CreateEmployee
    {
        private static readonly string cosmosConnectionString = Environment.GetEnvironmentVariable("CosmosDBConnectionString");
        private static readonly string databaseName = Environment.GetEnvironmentVariable("CosmosDBDatabaseName");
        private static readonly string containerName = Environment.GetEnvironmentVariable("CosmosDBContainerName");

        [FunctionName("CreateEmployee")]
        public static async Task<IActionResult> getEmployee(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "employee")] HttpRequest req,
    ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                log.LogInformation($"Request body: {requestBody}");

                // Validate JSON format and deserialize
                Employee employee;
                try
                {
                    employee = JsonConvert.DeserializeObject<Employee>(requestBody);
                }
                catch (JsonSerializationException ex)
                {
                    log.LogError($"Error deserializing request body: {ex.Message}");
                    return new BadRequestObjectResult("Invalid JSON format");
                }

                // Check for required properties (assuming "id" is mandatory)
                if (string.IsNullOrEmpty(employee.id))
                {
                    log.LogError("Missing required property: 'id'");
                    return new BadRequestObjectResult("Employee object missing required property 'id'");
                }

                if (employee == null)
                {
                    log.LogError("Failed to deserialize employee object from request body");
                    return new BadRequestObjectResult("Invalid employee data");
                }

                // Initialize Cosmos DB client
                CosmosClient client = new(cosmosConnectionString);
                Database database = client.GetDatabase(databaseName);
                Container container = database.GetContainer(containerName);

                //Determine partition key value based on employee data
                string partitionKeyValue = GetPartitionKeyValue(employee);
                // Create a partition key object
                PartitionKey partitionKey = new(partitionKeyValue);

                // Create new employee document in Cosmos DB with partition key
                await container.CreateItemAsync<Employee>(employee, partitionKey);
                
                return new OkObjectResult($"Employee record for employee id: {employee.id} created successfully");
            }
            catch (Exception ex)
            {
                log.LogError($"Error creating employee: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        private static string GetPartitionKeyValue(Employee employee)
        {
            // Example: Use employee department as partition key
            return employee.id;
        }
    }
}

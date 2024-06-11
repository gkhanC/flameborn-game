using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Data.Tables;
using Azure;
using System.Linq;

public static class AddDeviceDataFunction
{
    [FunctionName("AddDeviceDataFunction")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {       

        try
        {            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string deviceId = data?.DeviceId;
            string email = data?.EMail;
            string userName = data?.UserName;
            string password = data?.Password;
            int launchCount = data?.LaunchCount ?? 0;
            int rating = data?.Rating ?? 0;

            if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return new BadRequestObjectResult(new {success = false, message = "Please enter a valid Email Address, your password must be longer than 6 characters and contain at least 1 uppercase letter."});
            }
           
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "DeviceData");

            
            Pageable<TableEntity> queryResults = tableClient.Query<TableEntity>(filter: $"EMail eq '{email}'");

            if (queryResults.Any())
            {                
                return new OkObjectResult(new { success = false, message = "There is another account matching your Email address, a new account cannot be created. You can recover your account if you wish." });
            }
            
            var deviceData = new TableEntity
            {
                PartitionKey = "DeviceDataPartition",
                RowKey = Guid.NewGuid().ToString() + (new Random().Next(256) * (DateTime.Now.Millisecond)).ToString() + " - " + DateTime.Now.Date + DateTime.Now.ToShortTimeString(),
                ["DeviceId"] = deviceId,
                ["EMail"] = email,
                ["UserName"] = userName,
                ["Password"] = password,
                ["LaunchCount"] = launchCount,
                ["Rating"] = rating
            };

            
            await tableClient.AddEntityAsync(deviceData);
            
            return new OkObjectResult(new { success = true, message = "Your account has been successfully created." });
        }
        catch (Exception ex)
        {
            log.LogError($"Hata: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}

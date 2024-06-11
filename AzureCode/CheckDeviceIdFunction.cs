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
using System.Linq;
using Azure;

public static class CheckDeviceIdFunction
{
    [FunctionName("CheckDeviceIdFunction")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string deviceId = data?.DeviceId;

            if (string.IsNullOrEmpty(deviceId))
            {
                return new BadRequestObjectResult(new { success = false, message = "DeviceId is null or empty" });
            }

            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "DeviceData");

            Pageable<TableEntity> queryResults = tableClient.Query<TableEntity>(filter: $"DeviceId eq '{deviceId}'");

            bool exists = queryResults.Any();

            return new OkObjectResult(new { success = exists, message = "Device is found." });
        }
        catch (Exception ex)
        {
            log.LogError($"Hata: {ex.Message}\n{ex.StackTrace}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}

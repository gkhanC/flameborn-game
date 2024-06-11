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

public static class CheckDeviceIdAndEmailFunction
{
    [FunctionName("CheckDeviceIdAndEmailFunction")]
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

            if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(email))
            {
                return new BadRequestObjectResult(new { success = false, message = "Please enter a valid Email Address." });
            }

            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "DeviceData");

            Pageable<TableEntity> queryResults = tableClient.Query<TableEntity>(filter: $"DeviceId eq '{deviceId}' and EMail eq '{email}'");

            if (!queryResults.Any())
            {
                if (!tableClient.Query<TableEntity>(filter: $"EMail eq '{email}'").Any())
                {
                    return new OkObjectResult(new { success = false, email = false, device = false, message = "Your email address was not found in our records. You need to create a new account." });
                }

                if (!tableClient.Query<TableEntity>(filter: $"DeviceId eq '{deviceId}'").Any())
                {
                    return new OkObjectResult(new { success = false, email = true, device = false, message = "Are you logging in with another device? You need to recover your account to log in." });
                }

                var containEmail = tableClient.Query<TableEntity>(filter: $"EMail eq '{email}'").Any();
                var containDevice = tableClient.Query<TableEntity>(filter: $"DeviceId eq '{deviceId}'").Any();

                if (containEmail && !containDevice)
                {
                    return new OkObjectResult(new {success = false, email = true, device = false, message = "We detected a login from a new device. To log in, please recover your account."});
                }

            }

            bool exists = queryResults.Any();

            return new OkObjectResult(new { success = exists, email = true, device = true, message = "Information verified." });
        }
        catch (Exception ex)
        {
            log.LogError($"Hata: {ex.Message}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}

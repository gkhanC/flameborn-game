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

public static class GetRatingFunction
{
    [FunctionName("GetRatingFunction")]
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
            string password = data?.Password;

            if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                 return new BadRequestObjectResult(new { success = false, rating = 0, message = "Please enter a valid Email Address." });
            }

            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "DeviceData");

            Pageable<TableEntity> queryResults = tableClient.Query<TableEntity>(filter: $"DeviceId eq '{deviceId}' and EMail eq '{email}'");

            if (!queryResults.Any())
            {
               if (!tableClient.Query<TableEntity>(filter: $"EMail eq '{email}'").Any())
                {
                    return new OkObjectResult(new { success = false, rating = 0, message = "Your email address was not found in our records. You need to create a new account." });
                }

                if (!tableClient.Query<TableEntity>(filter: $"DeviceId eq '{deviceId}'").Any())
                {
                    return new OkObjectResult(new { success = false, rating = 0, message = "Are you logging in with another device? You need to recover your account to log in." });
                }
            }

            var entity = queryResults.First();

            string storedPassword = entity["Password"].ToString();
            if (storedPassword.Substring(0, 4) != password.Substring(0, 4))
            {
                return new OkObjectResult(new { success = false, rating = 0, message = "Geçersiz Password" });
            }

            int rating = int.Parse(entity["Rating"].ToString());

            return new OkObjectResult(new { success = true, rating = rating, message = $"Response saved. Rating {rating}" });
        }
        catch (Exception ex)
        {
            log.LogError($"Hata: {ex.Message}\n{ex.StackTrace}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}

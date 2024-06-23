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

namespace Company.Function
{
    public static class AddNewAccount
    {
        [FunctionName("AddNewAccount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var context = JsonConvert.DeserializeObject<FunctionExecutionContext<dynamic>>(requestBody);
            var data = context.FunctionArgument;

            string deviceId = data?.deviceId;
            string email = data?.email;
            string userName = data?.userName;
            string password = data?.password;
            int launchCount = data?.launchCount ?? 0;
            int rating = data?.rating ?? 0;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return new OkObjectResult(new { success = false, message = "Please enter a valid Email Address, your password must be longer than 6 characters and contain at least 1 uppercase letter." });
            }

            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "DeviceData");


            Pageable<TableEntity> queryResults = tableClient.Query<TableEntity>(filter: $"EMail eq '{email}'");

            if (queryResults.Any())
            {
                var entity = queryResults.First();

                if (!string.IsNullOrEmpty(deviceId) && entity["DeviceId"].ToString() != deviceId)
                {
                    entity["DeviceId"] = deviceId;
                }

                if (!string.IsNullOrEmpty(password) && entity["Password"].ToString() != password)
                {
                    entity["Password"] = password;
                }               

                if (launchCount > 0 && entity["LaunchCount"].ToString() != launchCount.ToString())
                {
                    entity["LaunchCount"] = launchCount;
                }

                if (rating > 0 && entity["Rating"].ToString() != rating.ToString())
                {
                    entity["Rating"] = rating;
                }

                await tableClient.UpdateEntityAsync(entity, ETag.All, TableUpdateMode.Replace);
                return new OkObjectResult(new { success = true, message = "Your Account Updated." });
            }

            var deviceData = new TableEntity
            {
                PartitionKey = "DeviceDataPartition",
                RowKey = Guid.NewGuid().ToString()[..5] + (new Random().Next(256) * (DateTime.Now.Millisecond)).ToString() + "-" + DateTime.Now.ToString("yyyyMMddTHHmmssZ"),
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
    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure;
using Azure.Data.Tables;
using System.Linq;
using PlayFab.ServerModels;

namespace Company.Function
{
    public class TitleAuthenticationContext
    {
        public string Id { get; set; }
        public string EntityToken { get; set; }
    }

    public class FunctionExecutionContext<T>
    {
        public PlayFab.ProfilesModels.EntityProfileBody CallEntityProfile { get; set; }
        public TitleAuthenticationContext TitleAuthenticationContext { get; set; }
        public bool? GeneratePlayStreamEvent { get; set; }
        public T FunctionArgument { get; set; }
    }

    public static class GetAccountInfo
    {
        [FunctionName("GetAccountInfo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Processing request to GetAccountInfo");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation("Request body: " + requestBody);

            var context = JsonConvert.DeserializeObject<FunctionExecutionContext<dynamic>>(requestBody);
            var data = context.FunctionArgument;

            string email = data?.email;
            string password = data?.password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new OkObjectResult(new { success = false, message = "Please enter a valid Email Address and Password." });
            }


            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "DeviceData");


            Pageable<TableEntity> queryResults = tableClient.Query<TableEntity>(filter: $"EMail eq '{email}' and Password eq '{password}'");

            if (!queryResults.Any())
            {
                if (!tableClient.Query<TableEntity>(filter: $"EMail eq '{email}'").Any())
                {
                    return new OkObjectResult(new { success = false, message = "Your email address was not found in our records. You need to create a new account." });
                }

                if (!tableClient.Query<TableEntity>(filter: $"Password eq '{password}'").Any())
                {
                    return new OkObjectResult(new { success = false, message = "Forgot your password?" });
                }
            }

            var entity = queryResults.First();

            string userName = entity["UserName"].ToString();
            int rating = int.Parse(entity["Rating"].ToString());
            int launchCount = int.Parse(entity["LaunchCount"].ToString());


            return new OkObjectResult(new { success = true, userName = userName, rating = rating, launchCount = launchCount, message = $"Response saved." });
        }
    }
}

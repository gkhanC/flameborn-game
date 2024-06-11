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

public static class UpdateDeviceDataFunction
{
    [FunctionName("UpdateDeviceDataFunction")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP tetiklemeli fonksiyon bir isteği işledi.");

        try
        {
            // İstek gövdesini oku
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string email = data?.EMail;
            string password = data?.Password;
            string newUserName = data?.NewUserName;
            string newDeviceId = data?.deviceId;
            int? newLaunchCount = data?.NewLaunchCount;
            int? newRating = data?.NewLaunchCount;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new BadRequestObjectResult("Lütfen istek gövdesine hem EMail hem de Password ekleyin");
            }

            // Azure Storage hesabı bağlantı dizesi
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "DeviceData");

            // Mevcut kaydı kontrol et
            Pageable<TableEntity> queryResults = tableClient.Query<TableEntity>(filter: $"EMail eq '{email}' and Password eq '{password}'");

            if (!queryResults.Any())
            {
                // Kayıt mevcut değilse, false döndür
                return new OkObjectResult(new { success = false, message = "Geçersiz EMail veya Password" });
            }

            // İlk bulunan kaydı al (email ve password unique kabul ediliyor)
            var entity = queryResults.First();

            // Yeni değerleri ata
            if (!string.IsNullOrEmpty(newUserName))
            {
                entity["UserName"] = newUserName;
            }

            if (!string.IsNullOrEmpty(newDeviceId))
            {
                entity["DeviceId"] = newDeviceId;
            }

            if (newLaunchCount.HasValue)
            {
                entity["LaunchCount"] = newLaunchCount.Value;
            }

            if (newLaunchCount.HasValue)
            {
                entity["Rating"] = newRating.Value;
            }

            // Kaydı güncelle
            await tableClient.UpdateEntityAsync(entity, ETag.All, TableUpdateMode.Replace);

            // Başarılı yanıt dön
            return new OkObjectResult(new { success = true });
        }
        catch (Exception ex)
        {
            log.LogError($"Hata: {ex.Message}\n{ex.StackTrace}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}

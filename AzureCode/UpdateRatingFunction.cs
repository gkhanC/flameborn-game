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

public static class UpdateRatingFunction
{
    [FunctionName("UpdateRatingFunction")]
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
            string deviceId = data?.DeviceId;
            string email = data?.EMail;
            string password = data?.Password;
            int newRating = data?.NewRating;

            if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return new BadRequestObjectResult("Lütfen istek gövdesine DeviceId, EMail, Password ve NewRating ekleyin");
            }

            // Azure Storage hesabı bağlantı dizesi
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            TableServiceClient tableServiceClient = new TableServiceClient(connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: "DeviceData");

            // DeviceId ve Email bilgilerini kontrol eden sorgu
            Pageable<TableEntity> queryResults = tableClient.Query<TableEntity>(filter: $"DeviceId eq '{deviceId}' and EMail eq '{email}'");

            if (!queryResults.Any())
            {
                if (!tableClient.Query<TableEntity>(filter: $"DeviceId eq '{deviceId}'").Any())
                {
                    return new OkObjectResult(new { success = false, message = "Geçersiz DeviceId" });
                }

                if (!tableClient.Query<TableEntity>(filter: $"EMail eq '{email}'").Any())
                {
                    return new OkObjectResult(new { success = false, message = "Geçersiz EMail" });
                }
            }

            // İlk bulunan kaydı al (DeviceId ve Email eşleşmeli)
            var entity = queryResults.First();

            // Password'ün ilk 4 karakterini kontrol et
            string storedPassword = entity["Password"].ToString();
            if (storedPassword.Substring(0, 4) != password.Substring(0, 4))
            {
                return new OkObjectResult(new { success = false, message = "Geçersiz Password" });
            }

            // Rating değerini güncelle
            entity["Rating"] = newRating;

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

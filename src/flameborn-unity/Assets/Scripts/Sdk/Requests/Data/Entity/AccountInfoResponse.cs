using System;
using flameborn.Sdk.Requests.Data.Abstract;
using flameborn.Sdk.Requests.Entity;

namespace flameborn.Sdk.Requests.Data.Entity
{
    [Serializable]
    public class AccountInfoResponse : ResponseEntity, IAccountInfoResponse
    {
        public string UserName { get; set; } = string.Empty;

        public int Rating { get; set; } = 0;

        public int LaunchCount { get; set; } = 0;

        public void SetResponse<T>(bool isSuccess, string userName, int rating, int launchCount, T response, string message = "")
        {
            UserName = userName;
            Rating = rating;
            LaunchCount = launchCount;
            SetResponse(isSuccess, response, message);
        }
    }
}
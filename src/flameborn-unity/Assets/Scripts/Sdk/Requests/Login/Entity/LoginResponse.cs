using System;
using flameborn.Sdk.Requests.Entity;
using flameborn.Sdk.Requests.Login.Abstract;

namespace flameborn.Sdk.Requests.Login.Entity
{
    [Serializable]
    public class LoginResponse : ResponseEntity, ILoginResponse
    {
        public bool IsAccountLogged { get; set; } = false;
        public bool NewlyCreated { get; set; } = false;
        public string PlayFabId { get; set; } = string.Empty;

        public LoginResponse()
        {

        }

        public void SetResponse<T>(bool isSuccess, bool isAccountLogged, bool isNewly, string fabId, T response, string message = "")
        {
            IsAccountLogged = isAccountLogged;
            NewlyCreated = isNewly;
            PlayFabId = fabId;
            SetResponse(isSuccess, response, message);
        }
    }
}
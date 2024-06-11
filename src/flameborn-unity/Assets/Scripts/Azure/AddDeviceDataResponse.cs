using System;

namespace Flameborn.Azure
{
    [Serializable]
    internal class AddDeviceDataResponse
    {
        public bool success;
        public string email;
        public string password;
        public string message;
    }
}
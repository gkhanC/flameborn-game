using System;

namespace Flameborn.Azure
{
    [Serializable]
    internal class CheckDeviceIdResponse
    {
        public bool success = false;
        public string message;
    }
}
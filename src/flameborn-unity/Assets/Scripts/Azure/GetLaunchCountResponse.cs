using System;

namespace Flameborn.Azure
{
    [Serializable]
    internal class GetLaunchCountResponse
    {
        public bool success;
        public int launchCount;
        public string message;
    }
}
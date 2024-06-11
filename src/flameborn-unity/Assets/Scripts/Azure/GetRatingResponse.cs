using System;

namespace Flameborn.Azure
{
    [Serializable]
    internal class GetRatingResponse
    {
        public bool success;
        public int rating;
        public string message;
    }
}
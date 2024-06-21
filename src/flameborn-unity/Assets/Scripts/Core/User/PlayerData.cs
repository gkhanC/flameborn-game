using System;
using System.Drawing;

namespace flameborn
{
    [Serializable]
    public class PlayerData
    {
        public string UserName { get; private set; }
        public string FabId { get; private set; }
        public string Rating { get; private set; }
        public string Rank { get; private set; }     

        public PlayerData(string fabId, string userName, string rating, string rank)
        {
            FabId = fabId;
            UserName = userName;
            Rating = rating;
            Rank = rank;
        }
    }
}
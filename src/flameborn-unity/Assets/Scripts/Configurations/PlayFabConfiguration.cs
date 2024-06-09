using System;
using Flameborn.User;

namespace Flameborn.Configurations
{
    public class PlayFabConfiguration : Configuration, IConfiguration
    {
        public string TitleId { get; set; } = String.Empty;
        public UserData UserData { get; set; } = new UserData();

        public PlayFabConfiguration(string configurationPath) : base(configurationPath)
        {
        }
    }
}

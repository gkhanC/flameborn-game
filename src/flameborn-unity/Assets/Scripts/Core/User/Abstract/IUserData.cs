using System.Linq;

namespace flameborn.Core.User.Abstract
{
    public interface IUserData
    {
        string UserName { get; set; }
        int Rating { get; set; }
        int Rank { get; set; }
        int LaunchCount { get; set; }
    }
}
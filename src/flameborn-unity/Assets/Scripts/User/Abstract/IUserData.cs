namespace Flameborn.User
{
    interface IUserData
    {
        bool IsRegistered { get; }
        string EMail { get; }
        string UserName { get; }
        string DeviceId { get; }
        int LaunchCount { get; }
    }
}
namespace Flameborn.Device.Abstract
{
    public interface IDeviceData
    {
        string DeviceId { get; }
        string EMail { get; }
        string Password { get; }
        string UserName { get; }
        int LaunchCount { get; }
        int Rating { get; }

        IDeviceData SetEmail(string email);
        IDeviceData SetPassword(string password);
        IDeviceData SetUserName(string userName);
        IDeviceData SetLaunchCount(int launchCount);
        IDeviceData SetRating(int rating);

    }
}
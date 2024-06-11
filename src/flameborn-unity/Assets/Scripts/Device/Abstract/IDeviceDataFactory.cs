using System.Collections.Generic;

namespace Flameborn.Device.Abstract
{
    public interface IDeviceDataFactory
    {
        IDeviceDataFactory SetEmail(string email);
        IDeviceDataFactory SetPassword(string password);
        IDeviceDataFactory SetUserName(string userName);
        IDeviceDataFactory SetLaunchCount(int launchCount);
        IDeviceDataFactory SetRating(int rating);
        (DeviceData deviceData, List<string> errorLogs) Create();

    }
}
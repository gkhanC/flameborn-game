using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for AddDeviceDataRequestController, provides method to post request for adding device data.
    /// </summary>
    public interface IAddDeviceDataRequestController
    {
        /// <summary>
        /// Posts request to add device data with the specified parameters.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="userName">The username associated with the device.</param>
        /// <param name="password">The password associated with the device.</param>
        /// <param name="launchCount">The launch count of the device.</param>
        /// <param name="rating">The rating of the device.</param>
        Task PostRequestAddDeviceData(string email, string userName, string password, int launchCount = 1, int rating = 0);
    }
}

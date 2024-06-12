using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for UpdateDeviceDataController, provides method to post request for updating device data.
    /// </summary>
    public interface IUpdateDeviceDataController
    {
        /// <summary>
        /// Posts request to update device data with the specified email and password.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="password">The password associated with the device.</param>
        Task PostRequestUpdateDeviceData(string email, string password);
    }
}

using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for validating the device ID.
    /// </summary>
    public interface IValidateDeviceIdController
    {
        /// <summary>
        /// Posts request to validate the device ID.
        /// </summary>
        Task PostRequestValidateDeviceId();
    }
}

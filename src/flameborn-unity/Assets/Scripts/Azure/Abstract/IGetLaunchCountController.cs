using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for GetLaunchCountController, provides method to post request for getting launch count.
    /// </summary>
    public interface IGetLaunchCountController
    {
        /// <summary>
        /// Posts request to get launch count with the specified email and password.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="password">The password associated with the device.</param>
        Task PostRequestGetLaunchCount(string email, string password, bool isHash = false);
    }
}

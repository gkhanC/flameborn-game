using System.Threading.Tasks;
using UnityEngine.Events;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for UpdateLaunchCountController, provides method to post request for updating launch count.
    /// </summary>
    public interface IUpdateLaunchCountController
    {
        /// <summary>
        /// Posts request to update launch count with the specified email, password, and new launch count.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="password">The password associated with the device.</param>
        /// <param name="newLaunchCount">The new launch count for the device.</param>
        Task PostRequestUpdateLaunchCount(string email, string password, int newLaunchCount);
    }
}

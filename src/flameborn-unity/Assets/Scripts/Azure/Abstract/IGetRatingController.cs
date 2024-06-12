using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for GetRatingController, provides method to post request for getting rating.
    /// </summary>
    public interface IGetRatingController
    {
        /// <summary>
        /// Posts request to get rating with the specified email and password.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="password">The password associated with the device.</param>
        Task PostRequestGetRating(string email, string password, bool isHash = false);
    }
}

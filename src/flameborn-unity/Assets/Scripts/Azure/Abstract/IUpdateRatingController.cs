using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for updating the rating.
    /// </summary>
    public interface IUpdateRatingController
    {
        /// <summary>
        /// Posts request to update rating with the specified email, password, and rating.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="password">The password associated with the device.</param>
        /// <param name="rating">The rating to be updated.</param>
        Task PostRequestUpdateRating(string email, string password, int rating, bool isHash = false);
    }
}

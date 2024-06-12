using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for updating the user password.
    /// </summary>
    public interface IUpdateUserPasswordController
    {
        /// <summary>
        /// Posts request to update user password with the specified email and password.
        /// </summary>
        /// <param name="email">The email associated with the device.</param>
        /// <param name="newPassword">The password to be updated.</param>
        Task PostRequestUpdateUserPassword(string email, string newPassword);
    }
}

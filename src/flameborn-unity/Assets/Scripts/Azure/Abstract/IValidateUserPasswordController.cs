using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for validating the user password.
    /// </summary>
    public interface IValidateLoginController
    {
        /// <summary>
        /// Posts request to validate the user password with the specified email and password.
        /// </summary>
        /// <param name="email">The email associated with the user.</param>
        /// <param name="password">The password to be validated.</param>
        Task PostRequestValidateUserPassword(string email, string password, bool isHash = false);
    }
}

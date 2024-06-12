using System.Threading.Tasks;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for validating the user email.
    /// </summary>
    public interface IValidateUserEmailController
    {
        /// <summary>
        /// Posts request to validate the user email.
        /// </summary>
        /// <param name="email">The email to be validated.</param>
        Task PostRequestValidateUserEmail(string email);
    }
}

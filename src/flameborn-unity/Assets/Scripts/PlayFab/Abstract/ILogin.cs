using MADD;

namespace Flameborn.PlayFab.Abstract
{
    /// <summary>
    /// Interface for handling login operations.
    /// </summary>
    [Docs("Interface for handling login operations.")]
    public interface ILogin
    {
        /// <summary>
        /// Logs the user in.
        /// </summary>
        /// <param name="logMessage">Outputs a log message indicating the result of the login attempt.</param>
        /// <returns>True if the login was successful, otherwise false.</returns>
        [Docs("Logs the user in and outputs a log message indicating the result of the login attempt.")]
        bool Login(out string logMessage);
    }
}

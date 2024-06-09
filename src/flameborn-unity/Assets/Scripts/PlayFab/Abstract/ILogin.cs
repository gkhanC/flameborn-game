namespace Flameborn.PlayFab.Abstract
{
    /// <summary>
    /// Interface for handling login operations.
    /// </summary>
    public interface ILogin
    {
        /// <summary>
        /// Logs the user in.
        /// </summary>
        /// <param name="logMessage">Outputs a log message indicating the result of the login attempt.</param>
        /// <returns>True if the login was successful, otherwise false.</returns>
        bool Login(out string logMessage);
    }
}

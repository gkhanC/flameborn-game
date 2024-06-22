using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Login.Abstract
{
    /// <summary>
    /// Defines an interface for login responses.
    /// </summary>
    public interface ILoginResponse : IApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the account is logged in.
        /// </summary>
        bool IsAccountLogged { get; }

        /// <summary>
        /// Gets a value indicating whether the account is newly created.
        /// </summary>
        bool NewlyCreated { get; }

        /// <summary>
        /// Gets the PlayFab ID associated with the account.
        /// </summary>
        string PlayFabId { get; }

        #endregion
    }
}

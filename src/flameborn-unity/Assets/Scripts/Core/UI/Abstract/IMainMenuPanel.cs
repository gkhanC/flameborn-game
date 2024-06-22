using flameborn.Core.User;

namespace flameborn.Core.UI.Abstract
{
    /// <summary>
    /// Defines an interface for the main menu panel.
    /// </summary>
    public interface IMainMenuPanel : IPanel
    {
        #region Properties

        /// <summary>
        /// Gets the login menu associated with the main menu panel.
        /// </summary>
        LoginPanel LoginMenu { get; }

        /// <summary>
        /// Gets the profile menu associated with the main menu panel.
        /// </summary>
        ProfilePanel ProfileMenu { get; }

        /// <summary>
        /// Gets the recovery menu associated with the main menu panel.
        /// </summary>
        RecoveryPanel RecoveryMenu { get; }

        /// <summary>
        /// Gets the register menu associated with the main menu panel.
        /// </summary>
        RegisterPanel RegisterMenu { get; }

        /// <summary>
        /// Gets the user data associated with the main menu panel.
        /// </summary>
        UserData UserData { get; }

        #endregion
    }
}

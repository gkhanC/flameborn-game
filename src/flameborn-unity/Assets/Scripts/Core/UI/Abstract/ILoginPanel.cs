namespace flameborn.Core.UI.Abstract
{
    /// <summary>
    /// Defines an interface for the login panel.
    /// </summary>
    public interface ILoginPanel : IPanel
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the email is valid.
        /// </summary>
        bool IsEmailValid { get; }

        /// <summary>
        /// Gets a value indicating whether the password is valid.
        /// </summary>
        bool IsPasswordValid { get; }

        /// <summary>
        /// Gets the recovery panel associated with the login panel.
        /// </summary>
        IPanel RecoveryPanel { get; }

        /// <summary>
        /// Gets the register panel associated with the login panel.
        /// </summary>
        IPanel RegisterPanel { get; }

        #endregion
    }
}

namespace flameborn.Core.UI.Abstract
{
    /// <summary>
    /// Defines an interface for the register panel.
    /// </summary>
    public interface IRegisterPanel : IPanel
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
        /// Gets a value indicating whether the user name is valid.
        /// </summary>
        bool IsUserNameValid { get; }

        /// <summary>
        /// Gets the login panel associated with the register panel.
        /// </summary>
        IPanel LoginPanel { get; }

        #endregion
    }
}

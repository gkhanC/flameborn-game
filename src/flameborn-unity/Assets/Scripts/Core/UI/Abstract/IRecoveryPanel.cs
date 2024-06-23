namespace flameborn.Core.UI.Abstract
{
    /// <summary>
    /// Defines an interface for the recovery panel.
    /// </summary>
    public interface IRecoveryPanel : IPanel
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether the email is valid.
        /// </summary>
        bool IsEmailValid { get; }

        /// <summary>
        /// Gets the login panel associated with the recovery panel.
        /// </summary>
        IPanel LoginPanel { get; }

        /// <summary>
        /// Gets the register panel associated with the recovery panel.
        /// </summary>
        IPanel RegisterPanel { get; }

        #endregion
    }
}

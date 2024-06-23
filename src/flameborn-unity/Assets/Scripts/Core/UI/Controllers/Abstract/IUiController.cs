using flameborn.Core.UI.Abstract;

namespace flameborn.Core.UI.Controller.Abstract
{
    /// <summary>
    /// Defines an interface for a UI controller that manages a specific type of panel.
    /// </summary>
    /// <typeparam name="T">The type of panel managed by the controller.</typeparam>
    public interface IUiController<T> : IPanel where T : IPanel
    {
        #region Properties

        /// <summary>
        /// Gets the panel managed by the controller.
        /// </summary>
        T Panel { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the panel managed by the controller.
        /// </summary>
        /// <param name="panel">The panel to be managed.</param>
        void SetPanel(T panel);

        #endregion
    }
}

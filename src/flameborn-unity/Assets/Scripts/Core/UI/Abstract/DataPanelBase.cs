namespace flameborn.Core.UI.Abstract
{
    /// <summary>
    /// Defines a base class for data panels with generic type.
    /// </summary>
    /// <typeparam name="T">The type of data managed by the panel.</typeparam>
    public abstract class DataPanelBase<T> : PanelBase, IDataPanel<T> where T : class, new()
    {
        #region Methods

        /// <summary>
        /// Event listener for when data has changed.
        /// </summary>
        /// <param name="values">The new data values.</param>
        public abstract void EventListener_OnDataHasChanged(T values);

        /// <summary>
        /// Initializes the panel with the specified data.
        /// </summary>
        /// <param name="value">The data to initialize the panel with.</param>
        public abstract void Init(T value);

        #endregion
    }
}

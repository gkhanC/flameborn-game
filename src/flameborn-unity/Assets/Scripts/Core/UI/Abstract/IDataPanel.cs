namespace flameborn.Core.UI.Abstract
{
    /// <summary>
    /// Defines an interface for data panels that manage data of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of data managed by the panel.</typeparam>
    public interface IDataPanel<T> : IPanel where T : class, new()
    {
        /// <summary>
        /// Event listener for when data has changed.
        /// </summary>
        /// <param name="value">The new data value.</param>
        void EventListener_OnDataHasChanged(T value);

        /// <summary>
        /// Initializes the panel with the specified data.
        /// </summary>
        /// <param name="values">The data to initialize the panel with.</param>
        void Init(T values);
    }
}

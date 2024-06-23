using System;

namespace flameborn.Core.Managers.Abstract
{
    /// <summary>
    /// Defines an interface for a game manager that handles other managers.
    /// </summary>
    public interface IGameManager : IManager
    {
        /// <summary>
        /// Retrieves a manager of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of manager to retrieve.</typeparam>
        /// <returns>A tuple containing a boolean indicating if the manager is present and the manager instance.</returns>
        (bool IsContain, T Instance) GetManager<T>() where T : IManager;

        /// <summary>
        /// Sets a manager of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of manager to set.</typeparam>
        /// <param name="concrete">The manager instance to set.</param>
        void SetManager<T>(T concrete) where T : IManager;
    }
}

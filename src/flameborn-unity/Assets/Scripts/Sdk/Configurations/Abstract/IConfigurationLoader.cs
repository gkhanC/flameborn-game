using System;

namespace flameborn.Sdk.Configurations.Abstract
{
    /// <summary>
    /// Defines an interface for loading configurations.
    /// </summary>
    public interface IConfigurationLoader
    {
        #region Methods

        /// <summary>
        /// Loads a configuration and processes it with the specified listeners.
        /// </summary>
        /// <typeparam name="T">The type of configuration to load.</typeparam>
        /// <param name="processListeners">The listeners to process the configuration.</param>
        void LoadConfiguration<T>(params Action<T>[] processListeners) where T : IConfiguration;

        #endregion
    }
}

namespace Flameborn.Configurations
{
    /// <summary>
    /// Interface for managing configuration controllers.
    /// </summary>
    public interface IConfigurationController<T>
    {
        /// <summary>
        /// Gets the configuration instance.
        /// </summary>       
        IConfiguration Configuration { get; }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="errorLog">Outputs error log if the load fails.</param>
        /// <returns>True if the configuration was loaded successfully, otherwise false.</returns>
        bool LoadConfiguration(out string errorLog);

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <param name="errorLog">Outputs error log if the save fails.</param>
        /// <param name="configuration">The configuration to save.</param>
        /// <returns>True if the configuration was saved successfully, otherwise false.</returns>
        bool SaveConfiguration(out string errorLog, T configuration);
    }
}

namespace Flameborn.Configurations
{
    /// <summary>
    /// Interface for managing configuration settings.
    /// </summary>    
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the path to the configuration file.
        /// </summary>        
        string ConfigurationFilePath { get; }
    }
}

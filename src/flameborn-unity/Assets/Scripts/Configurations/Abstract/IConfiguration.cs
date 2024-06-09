using MADD;

namespace Flameborn.Configurations
{
    /// <summary>
    /// Interface for managing configuration settings.
    /// </summary>
    [Docs("Interface for managing configuration settings.")]
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the path to the configuration file.
        /// </summary>
        [Docs("Gets the path to the configuration file.")]
        string ConfigurationFilePath { get; }
    }
}

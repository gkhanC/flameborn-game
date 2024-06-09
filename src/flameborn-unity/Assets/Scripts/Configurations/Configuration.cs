using MADD;

namespace Flameborn.Configurations
{
    /// <summary>
    /// Abstract base class for configuration handling.
    /// </summary>
    [Docs("Abstract base class for configuration handling.")]
    public abstract class Configuration : IConfiguration
    {
        /// <summary>
        /// The path to the configuration file.
        /// </summary>
        [Docs("The path to the configuration file.")]
        private readonly string _path;

        /// <summary>
        /// Gets the path to the configuration file.
        /// </summary>
        [Docs("Gets the path to the configuration file.")]
        public string ConfigurationFilePath => _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class with the specified path.
        /// </summary>
        /// <param name="path">The path to the configuration file.</param>
        [Docs("Initializes a new instance of the Configuration class with the specified path.")]
        protected Configuration(string path)
        {
            _path = path;
        }
    }
}

namespace Flameborn.Configurations
{
    /// <summary>
    /// Abstract base class for configuration handling.
    /// </summary>
    public abstract class Configuration : IConfiguration
    {
        /// <summary>
        /// The path to the configuration file.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Gets the path to the configuration file.
        /// </summary>
        public string ConfigurationFilePath => _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class with the specified path.
        /// </summary>
        /// <param name="path">The path to the configuration file.</param>
        protected Configuration(string path)
        {
            _path = path;
        }
    }
}

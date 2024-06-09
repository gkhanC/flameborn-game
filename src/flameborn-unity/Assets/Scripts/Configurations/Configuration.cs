namespace Flameborn.Configurations
{
    public abstract class Configuration : IConfiguration
    {
        private string _path;
        public string ConfigurationFilePath => _path;
        protected Configuration(string path)
        {
            _path = path;
        }

    }
}
namespace Flameborn.Configurations
{
    public interface IConfigurationController<T>
    {
        IConfiguration Configuration { get; }
        bool LoadConfiguration(out string errorLog);
        bool SaveConfiguration(out string errorLog, T configuration);
    }
}
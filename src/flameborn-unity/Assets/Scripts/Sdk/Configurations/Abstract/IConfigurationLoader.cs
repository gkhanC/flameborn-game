using System;

namespace flameborn.Sdk.Configurations.Abstract
{
    public interface IConfigurationLoader
    {
        void LoadConfiguration<T>(params Action<T>[] processListeners) where T : IConfiguration;
    }
}
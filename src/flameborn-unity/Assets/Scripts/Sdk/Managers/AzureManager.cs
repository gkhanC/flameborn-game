using flameborn.Core.Managers;
using flameborn.Core.Managers.Abstract;
using flameborn.Sdk.Configurations;
using HF.Library.Utilities.Singleton;
using HF.Logger;

namespace flameborn.Sdk.Managers
{
    public class AzureManager : MonoBehaviourSingleton<AzureManager>, IManager
    {
        AzureConfiguration configuration;

        private void Start()
        {            
            GameManager.Instance.SetManager(this);
            FindConfiguration();
        }

        private void FindConfiguration()
        {
            HFLogger.Log(this, $"{nameof(this.FindConfiguration)} invoked.");
            var manager = GameManager.Instance.GetManager<ConfigurationManager>();
            if (manager.IsContain)
            {
                manager.Instance.LoadConfiguration<AzureConfiguration>(this.SetConfiguration);
                return;
            }

            Invoke(nameof(this.FindConfiguration), 2f);
        }

        private void SetConfiguration(AzureConfiguration azureConfiguration)
        {
            configuration = azureConfiguration;
            HFLogger.Log(this, $"{azureConfiguration.GetType().Name} configuration file saved with {nameof(this.SetConfiguration)} function.");
        }
    }
}
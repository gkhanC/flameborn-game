using flameborn.Core.Managers;
using flameborn.Core.Managers.Abstract;
using flameborn.Sdk.Configurations;
using HF.Library.Utilities.Singleton;
using HF.Logger;

namespace flameborn.Sdk.Managers
{
    /// <summary>
    /// Manager for handling Azure configurations.
    /// </summary>
    public class AzureManager : MonoBehaviourSingleton<AzureManager>, IManager
    {
        #region Fields

        private AzureConfiguration configuration;

        #endregion

        #region Methods

        /// <summary>
        /// Called on the first frame the script is active.
        /// </summary>
        private void Start()
        {
            GameManager.Instance.SetManager(this);
            FindConfiguration();
        }

        /// <summary>
        /// Attempts to find and load the Azure configuration.
        /// </summary>
        private void FindConfiguration()
        {
            HFLogger.Log(this, $"{nameof(FindConfiguration)} invoked.");
            var manager = GameManager.Instance.GetManager<ConfigurationManager>();
            if (manager.IsContain)
            {
                manager.Instance.LoadConfiguration<AzureConfiguration>(SetConfiguration);
                return;
            }

            Invoke(nameof(FindConfiguration), 2f);
        }

        /// <summary>
        /// Sets the Azure configuration.
        /// </summary>
        /// <param name="azureConfiguration">The Azure configuration to set.</param>
        private void SetConfiguration(AzureConfiguration azureConfiguration)
        {
            configuration = azureConfiguration;
            HFLogger.Log(this, $"{azureConfiguration.GetType().Name} configuration file saved with {nameof(SetConfiguration)} function.");
        }

        #endregion
    }
}

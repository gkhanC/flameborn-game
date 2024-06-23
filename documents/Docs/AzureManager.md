
# AzureManager Class Documentation

## Overview
The `AzureManager` class manages Azure configurations within the Flameborn SDK. This class is a singleton derived from `MonoBehaviourSingleton` and implements the `IManager` interface.

## Class Definition

```csharp
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
```

## Fields
- **configuration**: An instance of the `AzureConfiguration` class representing the Azure configuration.

## Methods
### Public Methods
- **Start()**: Called on the first frame the script is active. Sets the manager and attempts to find the Azure configuration.

### Private Methods
- **FindConfiguration()**: Attempts to find and load the Azure configuration. Logs the attempt and retries if the configuration manager is not found.
- **SetConfiguration(AzureConfiguration azureConfiguration)**: Sets the Azure configuration and logs the success.

## Usage Example
Below is an example of how to use the `AzureManager` class in a Unity project.

```csharp
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        AzureManager.Instance.FindConfiguration();
    }
}
```

## See Also
For more information on the `IManager` interface, refer to the [IManager documentation](https://gkhanc.github.io/flameborn-game/IManager).

## File Location
This class is defined in the `AzureManager.cs` file, located in the `flameborn.Sdk.Managers` namespace.

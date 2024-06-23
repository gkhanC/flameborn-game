
# ConfigurationManager Class Documentation

## Overview
The `ConfigurationManager` class handles various configurations within the Flameborn SDK. This class is a singleton derived from `MonoBehaviourSingleton` and implements the `IConfigurationManager` and `IConfigurationLoader` interfaces.

## Class Definition

```csharp
using System;
using flameborn.Core.Managers;
using flameborn.Sdk.Configurations.Abstract;
using flameborn.Sdk.Managers.Abstract;
using HF.Library.Utilities.Singleton;
using HF.Logger;
using Newtonsoft.Json;
using Sirenix.Utilities;
using UnityEngine;

namespace flameborn.Sdk.Managers
{
    /// <summary>
    /// Manager for handling configurations.
    /// </summary>
    [Serializable]
    public class ConfigurationManager : MonoBehaviourSingleton<ConfigurationManager>, IConfigurationManager, IConfigurationLoader
    {
        #region Methods

        /// <summary>
        /// Called on the first frame the script is active.
        /// </summary>
        private void Start()
        {
            GameManager.Instance.SetManager(this);
        }

        /// <summary>
        /// Loads the configuration of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of configuration to load.</typeparam>
        /// <param name="processListeners">The listeners to process the configuration.</param>
        public void LoadConfiguration<T>(params Action<T>[] processListeners) where T : IConfiguration
        {
            var json = Resources.Load<TextAsset>(typeof(T).Name)?.ToString();

            if (string.IsNullOrEmpty(json)) return;

            var configuration = JsonConvert.DeserializeObject<T>(json);

            processListeners?.ForEach(a => a.Invoke(configuration));
            HFLogger.Log(this, $"{nameof(LoadConfiguration)} configuration load.");
        }

        #endregion
    }
}
```

## Methods
### Public Methods
- **Start()**: Called on the first frame the script is active. Sets the manager.

- **LoadConfiguration<T>(params Action<T>[] processListeners)**: Loads the configuration of type `T`. 
  - **Parameters**:
    - **T**: The type of configuration to load.
    - **processListeners**: The listeners to process the configuration.

## Usage Example
Below is an example of how to use the `ConfigurationManager` class in a Unity project.

```csharp
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        ConfigurationManager.Instance.LoadConfiguration<MyConfiguration>(config =>
        {
            Debug.Log("Configuration Loaded: " + config.SomeProperty);
        });
    }
}
```

## See Also
For more information on the `IConfigurationManager` interface, refer to the [IConfigurationManager documentation](https://gkhanc.github.io/flameborn-game/IConfigurationManager).

For more information on the `IConfigurationLoader` interface, refer to the [IConfigurationLoader documentation](https://gkhanc.github.io/flameborn-game/IConfigurationLoader).

## File Location
This class is defined in the `ConfigurationManager.cs` file, located in the `flameborn.Sdk.Managers` namespace.

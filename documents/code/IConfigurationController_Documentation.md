
# Flameborn.Configurations Namespace

## IConfigurationController<T> Interface

### Summary

The `IConfigurationController<T>` interface is designed for managing configuration controllers within the application. It provides methods for loading and saving configurations and ensures that any implementing class has a mechanism to handle configuration instances.

### Properties

| Property Name         | Type            | Access   | Description                                |
|-----------------------|-----------------|----------|--------------------------------------------|
| Configuration         | IConfiguration  | Read-only| Gets the configuration instance.           |

### Methods

| Method Name           | Return Type | Parameters                                                                                       | Description                                                                           |
|-----------------------|-------------|--------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------|
| LoadConfiguration     | bool        | `out string errorLog`                                                                            | Loads the configuration. Outputs an error log if the load fails.                      |
| SaveConfiguration     | bool        | `out string errorLog, T configuration`                                                           | Saves the configuration. Outputs an error log if the save fails.                      |

---

### Detailed Description

- **Namespace**: `Flameborn.Configurations`
  - The namespace `Flameborn.Configurations` groups related classes and interfaces together, promoting organized code structure.

### Interface: IConfigurationController<T>

```csharp
namespace Flameborn.Configurations
{
    /// <summary>
    /// Interface for managing configuration controllers.
    /// </summary>
    public interface IConfigurationController<T>
    {
        /// <summary>
        /// Gets the configuration instance.
        /// </summary>       
        IConfiguration Configuration { get; }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="errorLog">Outputs error log if the load fails.</param>
        /// <returns>True if the configuration was loaded successfully, otherwise false.</returns>
        bool LoadConfiguration(out string errorLog);

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        /// <param name="errorLog">Outputs error log if the save fails.</param>
        /// <param name="configuration">The configuration to save.</param>
        /// <returns>True if the configuration was saved successfully, otherwise false.</returns>
        bool SaveConfiguration(out string errorLog, T configuration);
    }
}
```

### Interface Description

The `IConfigurationController<T>` interface defines a contract for classes that manage configuration controllers. The primary purpose of this interface is to provide methods for loading and saving configurations while ensuring that any implementing class can handle configuration instances.

### Property: Configuration

| Attribute              | Value  |
|------------------------|--------|
| **Type**               | IConfiguration |
| **Access**             | Read-only |
| **Description**        | This property provides the configuration instance that is being managed. |

```csharp
/// <summary>
/// Gets the configuration instance.
/// </summary>       
IConfiguration Configuration { get; }
```

### Method: LoadConfiguration

| Attribute              | Value  |
|------------------------|--------|
| **Return Type**        | bool |
| **Parameters**         | `out string errorLog` |
| **Description**        | This method loads the configuration. It returns true if the configuration was loaded successfully, otherwise false. Outputs an error log if the load fails. |

```csharp
/// <summary>
/// Loads the configuration.
/// </summary>
/// <param name="errorLog">Outputs error log if the load fails.</param>
/// <returns>True if the configuration was loaded successfully, otherwise false.</returns>
bool LoadConfiguration(out string errorLog);
```

### Method: SaveConfiguration

| Attribute              | Value  |
|------------------------|--------|
| **Return Type**        | bool |
| **Parameters**         | `out string errorLog, T configuration` |
| **Description**        | This method saves the configuration. It returns true if the configuration was saved successfully, otherwise false. Outputs an error log if the save fails. |

```csharp
/// <summary>
/// Saves the configuration.
/// </summary>
/// <param name="errorLog">Outputs error log if the save fails.</param>
/// <param name="configuration">The configuration to save.</param>
/// <returns>True if the configuration was saved successfully, otherwise false.</returns>
bool SaveConfiguration(out string errorLog, T configuration);
```

This documentation provides a comprehensive overview of the `IConfigurationController<T>` interface, its purpose, properties, and methods. The provided code snippet illustrates how the interface and its members are defined in C#.

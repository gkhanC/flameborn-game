
# Configuration Documentation

## Overview

The `Configuration` class is an abstract base class for handling configuration files. It provides the basic structure and functionality for accessing the configuration file path.

## Namespace Used

```csharp
namespace Flameborn.Configurations
{
```

## Class: `Configuration`

### Summary
Abstract base class for configuration handling.

### Fields

| Field             | Type      | Description                       |
|-------------------|-----------|-----------------------------------|
| `_path`           | `string`  | The path to the configuration file.|

### Properties

| Property                  | Type      | Description                        |
|---------------------------|-----------|------------------------------------|
| `ConfigurationFilePath`   | `string`  | Gets the path to the configuration file. |

### Constructors

| Constructor               | Description                                                       |
|---------------------------|-------------------------------------------------------------------|
| `Configuration(string path)` | Initializes a new instance of the `Configuration` class with the specified path. |

### Detailed Constructor Descriptions

#### `Configuration`

```csharp
protected Configuration(string path)
```

- **Description**: Initializes a new instance of the `Configuration` class with the specified path.
- **Parameters**: 
  - `path`: The path to the configuration file.

### Usage Example

```csharp
public class MyConfiguration : Configuration
{
    public MyConfiguration(string path) : base(path)
    {
    }
}

var config = new MyConfiguration("/path/to/config/file");
Console.WriteLine(config.ConfigurationFilePath);
```

This documentation provides a comprehensive overview of the `Configuration` class, including its purpose, field descriptions, property descriptions, and usage example.

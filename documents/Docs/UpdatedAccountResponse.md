
# UpdatedAccountResponse Class Documentation

## Overview
The `UpdatedAccountResponse` class represents the response received after updating an account within the Flameborn SDK.

## Class Definition

```csharp
using Newtonsoft.Json;

namespace flameborn.Core.Accounts
{
    /// <summary>
    /// Represents the response received after updating an account.
    /// </summary>
    public class UpdatedAccountResponse
    {
        #region Properties

        /// <summary>
        /// Indicates whether the update was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        /// <summary>
        /// Gets or sets the message associated with the update response.
        /// </summary>
        [JsonProperty("Message")]
        public string Message { get; set; } = string.Empty;

        #endregion
    }
}
```

## Properties
- **Success**: Indicates whether the update was successful.
- **Message**: Gets or sets the message associated with the update response.

## Usage Example
Below is an example of how to use the `UpdatedAccountResponse` class in a Unity project.

```csharp
using flameborn.Core.Accounts;
using Newtonsoft.Json;
using UnityEngine;

public class ExampleUsage : MonoBehaviour
{
    private void Start()
    {
        string jsonResponse = "{ "success": true, "Message": "Account updated successfully." }";
        UpdatedAccountResponse response = JsonConvert.DeserializeObject<UpdatedAccountResponse>(jsonResponse);

        if (response.Success)
        {
            Debug.Log("Update Success: " + response.Message);
        }
        else
        {
            Debug.LogError("Update Failed: " + response.Message);
        }
    }
}
```

## File Location
This class is defined in the `UpdatedAccountResponse.cs` file, located in the `flameborn.Core.Accounts` namespace.


# IDataPanel.cs Documentation

## Overview
`IDataPanel<T>` is an interface for data panels that manage data of a specific type. It provides methods to handle data changes and initialize the panel with data.

## Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|------------|-------------|
| `EventListener_OnDataHasChanged(T value)` | Event listener for when data has changed. | `T value` | `void` |
| `Init(T values)` | Initializes the panel with the specified data. | `T values` | `void` |

## Example Usage

```csharp
public class MyDataPanel : IDataPanel<MyDataType>
{
    public void EventListener_OnDataHasChanged(MyDataType value)
    {
        // Handle data change
    }

    public void Init(MyDataType values)
    {
        // Initialize with data
    }
}
```

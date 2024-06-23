
# DataPanelBase.cs Documentation

## Overview
`DataPanelBase<T>` is an abstract class that serves as a base for data panels managing data of a specific type. It provides methods to handle data changes and initialize the panel with data.

## Public Methods

| Method Name | Description | Parameters | Return Type |
|-------------|-------------|------------|-------------|
| `EventListener_OnDataHasChanged(T values)` | Event listener for when data has changed. | `T values` | `void` |
| `Init(T value)` | Initializes the panel with the specified data. | `T value` | `void` |

## Example Usage

```csharp
public class ExampleDataPanel : DataPanelBase<MyDataType>
{
    public override void EventListener_OnDataHasChanged(MyDataType values)
    {
        // Handle data change
    }

    public override void Init(MyDataType value)
    {
        // Initialize with data
    }
}
```

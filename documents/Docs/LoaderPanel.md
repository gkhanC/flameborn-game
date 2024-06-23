
# LoaderPanel.cs Documentation

## Overview
`LoaderPanel` is a serializable class that represents a loader panel in the game UI. It inherits from `PanelBase` and provides methods to show and hide the loader panel.

## Example Usage

```csharp
public class ExampleUsage
{
    private LoaderPanel loaderPanel;

    public void InitializeLoaderPanel()
    {
        loaderPanel = new LoaderPanel();
    }

    public void ShowLoader()
    {
        loaderPanel.Show();
    }

    public void HideLoader()
    {
        loaderPanel.Hide();
    }
}
```

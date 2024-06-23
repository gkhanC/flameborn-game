
# BoxGroupAttribute Class Documentation

## Overview
The `BoxGroupAttribute` class is a custom attribute used to group properties within the Flameborn SDK. This attribute is derived from the `PropertyGroupAttribute` class provided by the Odin Inspector library. It allows grouping properties with a label and options to show and center the label.

## Class Definition

```csharp
using System;
using Sirenix.OdinInspector;

namespace flameborn.Core.Attributes
{
    /// <summary>
    /// Custom attribute to group properties with a label and options to show and center the label.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class BoxGroupAttribute : PropertyGroupAttribute
    {
        #region Properties

        /// <summary>
        /// Gets the label of the box group.
        /// </summary>
        public string Label { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the label should be shown.
        /// </summary>
        public bool ShowLabel { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the label should be centered.
        /// </summary>
        public bool CenterLabel { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BoxGroupAttribute"/> class.
        /// </summary>
        /// <param name="group">The label of the group.</param>
        /// <param name="showLabel">Indicates whether the label should be shown. Default is true.</param>
        /// <param name="centerLabel">Indicates whether the label should be centered. Default is false.</param>
        /// <param name="order">The order of the group. Default is 0.</param>
        public BoxGroupAttribute(string group, bool showLabel = true, bool centerLabel = false, float order = 0)
            : base(group, order)
        {
            this.Label = group;
            this.ShowLabel = showLabel;
            this.CenterLabel = centerLabel;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Combines the values with another <see cref="PropertyGroupAttribute"/> instance.
        /// </summary>
        /// <param name="other">The other <see cref="PropertyGroupAttribute"/> instance.</param>
        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            var attr = other as BoxGroupAttribute;

            if (this.Label == null)
            {
                this.Label = attr.Label;
            }

            this.ShowLabel |= attr.ShowLabel;
            this.CenterLabel |= attr.CenterLabel;
        }

        #endregion
    }
}
```

## Properties
- **Label**: Gets the label of the box group.
- **ShowLabel**: Gets a value indicating whether the label should be shown.
- **CenterLabel**: Gets a value indicating whether the label should be centered.

## Methods
### Public Methods
- **BoxGroupAttribute(string group, bool showLabel = true, bool centerLabel = false, float order = 0)**: Initializes a new instance of the `BoxGroupAttribute` class.
  - **Parameters**:
    - **group**: The label of the group.
    - **showLabel**: Indicates whether the label should be shown. Default is true.
    - **centerLabel**: Indicates whether the label should be centered. Default is false.
    - **order**: The order of the group. Default is 0.

### Protected Methods
- **CombineValuesWith(PropertyGroupAttribute other)**: Combines the values with another `PropertyGroupAttribute` instance.
  - **Parameters**:
    - **other**: The other `PropertyGroupAttribute` instance.

## Usage Example
Below is an example of how to use the `BoxGroupAttribute` class in a Unity project.

```csharp
using UnityEngine;
using flameborn.Core.Attributes;

public class ExampleUsage : MonoBehaviour
{
    [BoxGroup("Group A")]
    public int valueA;

    [BoxGroup("Group B", showLabel: false)]
    public int valueB;

    [BoxGroup("Group C", centerLabel: true)]
    public int valueC;
}
```

## See Also
For more information on the `PropertyGroupAttribute` class, refer to the [PropertyGroupAttribute documentation](https://gkhanc.github.io/flameborn-game/PropertyGroupAttribute).

## File Location
This class is defined in the `BoxGroupAttribute.cs` file, located in the `flameborn.Core.Attributes` namespace.

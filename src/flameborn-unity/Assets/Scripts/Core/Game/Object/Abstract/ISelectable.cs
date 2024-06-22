using flameborn.Core.Game.Events;
using UnityEngine;

namespace flameborn.Core.Game.Objects.Abstract
{
    /// <summary>
    /// Defines methods and properties for selectable objects.
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Gets the game object associated with the selectable object.
        /// </summary>
        GameObject GetGameObject { get; }

        /// <summary>
        /// Gets the selectable type of the object.
        /// </summary>
        SelectableTypes selectableType { get; }

        /// <summary>
        /// Called when the object is selected.
        /// </summary>
        void Select();

        /// <summary>
        /// Sets the target object.
        /// </summary>
        /// <param name="go">The target selectable object.</param>
        void SetTarget(ISelectable go);

        /// <summary>
        /// Sets the destination position for the object.
        /// </summary>
        /// <param name="position">The destination position.</param>
        void SetDestination(Vector3 position);

        /// <summary>
        /// Called when the object is deselected.
        /// </summary>
        void DeSelect();
    }
}

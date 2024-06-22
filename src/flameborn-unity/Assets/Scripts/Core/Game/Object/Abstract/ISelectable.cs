using flameborn.Core.Game.Events;
using UnityEngine;

namespace flameborn.Core.Game.Objects.Abstract
{
    public interface ISelectable
    {
        GameObject GetGameObject { get; }
        SelectableTypes selectableType { get; }
        void Select();
        void SetTarget(ISelectable go);
        void SetDestination(Vector3 position);
        void DeSelect();
    }
}
using HF.Logger;
using Sirenix.OdinInspector;
using UnityEngine;

namespace flameborn.Core.UI.Abstract
{
    public abstract class PanelBase : IPanel
    {
#nullable enable
        protected object? lockObj = default;
#nullable disable

        [FoldoutGroup("Panel Settings", expanded: true)]
        [field: SerializeField] public GameObject uiObject;

        public GameObject UIObject { get => uiObject; protected set => uiObject = value; }

        protected PanelBase() { }

        public virtual void Show() { if (lockObj != null) return; UIObject.SetActive(true); }

        public virtual void Hide() { if (lockObj != null) return; UIObject.SetActive(false); }

        public virtual void Lock(object lockerObject) { if (lockObj != null) return; lockObj = lockerObject; }

        public virtual void UnLock(object lockerObject) { if (lockObj == null) return; if (lockObj != lockerObject) return; lockObj = null; }
    }
}
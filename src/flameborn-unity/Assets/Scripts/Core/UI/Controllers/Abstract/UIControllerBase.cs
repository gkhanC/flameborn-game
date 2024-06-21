using flameborn.Core.UI.Abstract;
using HF.Logger;
using UnityEngine;

namespace flameborn.Core.UI.Controller.Abstract
{
    public abstract class UIControllerBase<T> : MonoBehaviour, IUiController<T> where T : IPanel
    {
        [field: SerializeField] public T Panel { get; protected set; }

        protected UIControllerBase() { }
        public void Hide() { Panel.Hide(); }
        public void Lock(object lockerObject) { Panel.Lock(lockerObject); }
        public void SetPanel(T panel) { Panel = panel; }
        public void Show() { Panel.Show(); }
        public void UnLock(object lockerObject) { Panel.UnLock(lockerObject); }
    }
}
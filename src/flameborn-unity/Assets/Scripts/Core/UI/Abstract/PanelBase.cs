using HF.Logger;
using Sirenix.OdinInspector;
using UnityEngine;

namespace flameborn.Core.UI.Abstract
{
    /// <summary>
    /// Defines a base class for all UI panels.
    /// </summary>
    public abstract class PanelBase : IPanel
    {
        #region Fields

#nullable enable
        protected object? lockObj = default;
#nullable disable

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the UI object associated with the panel.
        /// </summary>
        [FoldoutGroup("Panel Settings", expanded: true)]
        [field: SerializeField] public GameObject uiObject;
        
        public GameObject UIObject 
        {
            get => uiObject; 
            protected set => uiObject = value; 
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelBase"/> class.
        /// </summary>
        protected PanelBase() { }

        #endregion

        #region Methods

        /// <summary>
        /// Shows the panel if it is not locked.
        /// </summary>
        public virtual void Show() 
        {
            if (lockObj != null) return; 
            UIObject.SetActive(true); 
        }

        /// <summary>
        /// Hides the panel if it is not locked.
        /// </summary>
        public virtual void Hide() 
        {
            if (lockObj != null) return; 
            UIObject.SetActive(false); 
        }

        /// <summary>
        /// Locks the panel with the specified object.
        /// </summary>
        /// <param name="lockerObject">The object used to lock the panel.</param>
        public virtual void Lock(object lockerObject) 
        {
            if (lockObj != null) return; 
            lockObj = lockerObject; 
        }

        /// <summary>
        /// Unlocks the panel if the specified object matches the lock object.
        /// </summary>
        /// <param name="lockerObject">The object used to unlock the panel.</param>
        public virtual void UnLock(object lockerObject) 
        {
            if (lockObj == null) return; 
            if (lockObj != lockerObject) return; 
            lockObj = null; 
        }

        #endregion
    }
}

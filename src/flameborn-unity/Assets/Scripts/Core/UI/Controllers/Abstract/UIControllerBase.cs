using flameborn.Core.UI.Abstract;
using HF.Logger;
using UnityEngine;

namespace flameborn.Core.UI.Controller.Abstract
{
    /// <summary>
    /// Defines a base class for UI controllers that manage a specific type of panel.
    /// </summary>
    /// <typeparam name="T">The type of panel managed by the controller.</typeparam>
    public abstract class UIControllerBase<T> : MonoBehaviour, IUiController<T> where T : IPanel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the panel managed by the controller.
        /// </summary>
        [field: SerializeField] public T Panel { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UIControllerBase{T}"/> class.
        /// </summary>
        protected UIControllerBase() { }

        #endregion

        #region Methods

        /// <summary>
        /// Hides the panel.
        /// </summary>
        public void Hide() 
        { 
            Panel.Hide(); 
        }

        /// <summary>
        /// Locks the panel with the specified object.
        /// </summary>
        /// <param name="lockerObject">The object used to lock the panel.</param>
        public void Lock(object lockerObject) 
        { 
            Panel.Lock(lockerObject); 
        }

        /// <summary>
        /// Sets the panel managed by the controller.
        /// </summary>
        /// <param name="panel">The panel to be managed.</param>
        public void SetPanel(T panel) 
        { 
            Panel = panel; 
        }

        /// <summary>
        /// Shows the panel.
        /// </summary>
        public void Show() 
        { 
            Panel.Show(); 
        }

        /// <summary>
        /// Unlocks the panel if the specified object matches the lock object.
        /// </summary>
        /// <param name="lockerObject">The object used to unlock the panel.</param>
        public void UnLock(object lockerObject) 
        { 
            Panel.UnLock(lockerObject); 
        }

        #endregion
    }
}

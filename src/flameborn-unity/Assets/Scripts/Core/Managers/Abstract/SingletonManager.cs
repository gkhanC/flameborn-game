using System.ComponentModel;
using HF.Library.Utilities.Singleton;
using UnityEngine;

namespace flameborn.Core.Managers.Abstract
{
    /// <summary>
    /// Defines a base class for singleton managers in the system.
    /// </summary>
    /// <typeparam name="T">The type of the singleton manager.</typeparam>
    public abstract class SingletonManager<T> : MonoBehaviourSingleton<T> where T : MonoBehaviour, IManager
    {
        #region Properties

        /// <summary>
        /// Gets the singleton instance of the manager.
        /// </summary>
        public static T Instance => GetInstance();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonManager{T}"/> class.
        /// </summary>
        protected SingletonManager()
        {
            // Protected constructor to prevent instantiation.
        }

        #endregion
    }
}

using System.ComponentModel;
using HF.Library.Utilities.Singleton;
using UnityEngine;

namespace flameborn.Core.Managers.Abstract
{
    public abstract class SingletonManager<T> : MonoBehaviourSingleton<T> where T : MonoBehaviour, IManager
    {
        public static T Instance => GetInstance();
        protected SingletonManager()
        {

        }
    }
}
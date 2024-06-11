namespace Flameborn.Managers
{
    using UnityEngine;

    public abstract class SceneLogic : MonoBehaviour
    {
        [SerializeField]
        protected float initTime = 1f;

        /// <summary>
        /// Singleton instance of GameManager.
        /// </summary>
        public static SceneLogic Instance { get; private set; }

        protected virtual void Awake()
        {
            Instance = this;
        }

        protected virtual void Start()
        {
            Invoke(nameof(this.Initialize), initTime);
        }

        public abstract void Initialize();
    }
}
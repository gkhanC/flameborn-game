using HF.Extensions;
using MADD;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

namespace Flameborn.Manager
{
    /// <summary>
    /// Manages the user interface elements and their interactions.
    /// </summary>
    [Docs("Manages the user interface elements and their interactions.")]
    public class UIManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of UIManager.
        /// </summary>
        [Docs("Singleton instance of UIManager.")]
        public static UIManager Instance { get; private set; }

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        [Docs("Called when the script instance is being loaded.")]
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        [Docs("Called on the frame when a script is enabled just before any of the Update methods are called the first time.")]
        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// Called when a scene is loaded.
        /// </summary>
        /// <param name="scene">The scene that was loaded.</param>
        /// <param name="mode">The mode in which the scene was loaded.</param>
        [Docs("Called when a scene is loaded.")]
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Implementation of scene loading logic goes here.
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
        [Docs("Called when the object becomes enabled and active.")]
        private void OnEnable()
        {
            if (Instance.IsNull() || Instance.gameObject.IsNull())
            {
                Instance = this;
            }
            else
            {
                if (!Instance.gameObject.Equals(gameObject))
                {
                    DestroyImmediate(gameObject);
                }
                else if (!Instance.Equals(this))
                {
                    DestroyImmediate(this);
                }
            }
        }
    }
}

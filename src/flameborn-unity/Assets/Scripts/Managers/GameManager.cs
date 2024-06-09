using HF.Extensions;
using HF.Logger;
using HF.Logger.FileLogger;
using MADD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flameborn.Manager
{
    /// <summary>
    /// This class manages the game lifecycle and logging.
    /// </summary>
    [Docs("This class manages the game lifecycle and logging.")]
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The path where log files are saved.
        /// </summary>
        [Docs("The path where log files are saved.")]
        [SerializeField]
        public readonly string fileLoggingPath = @"C:\Users\gkhan\Documents\Development\Games\flameborn-game\logs\Logs.md";

        /// <summary>
        /// Singleton instance of GameManager.
        /// </summary>
        [Docs("Singleton instance of GameManager.")]
        public static GameManager Instance { get; private set; }

        /// <summary>
        /// File logger instance.
        /// </summary>
        private FileLog fileLog;

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        [Docs("Called when the script instance is being loaded.")]
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            fileLog = new FileLog(logFilePath: fileLoggingPath);
            HFLogger.AddLogger(fileLog);
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        [Docs("Called on the frame when a script is enabled just before any of the Update methods are called the first time.")]
        private void Start()
        {
            HFLogger.Log(this, $"Start Game from {SceneManager.GetActiveScene().name}");
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

        /// <summary>
        /// Called when the behaviour becomes disabled or inactive.
        /// </summary>
        [Docs("Called when the behaviour becomes disabled or inactive.")]
        private void OnDisable()
        {
            HFLogger.RemoveLogger(fileLog);
        }
    }
}

using HF.Extensions;
using HF.Logger;
using HF.Logger.FileLogger;
using MADD;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flameborn.Manager
{
    [Docs("This class manages the game lifecycle and logging.")]
    public class GameManager : MonoBehaviour
    {
        [Docs("The path where log files are saved.")]
        [SerializeField]
        public readonly string fileLoggingPath = @"C:\Users\gkhan\Documents\Development\Games\flameborn-game\logs\Logs.md";

        [Docs("Singleton instance of GameManager.")]
        public static GameManager Instance { get; private set; }

        private FileLog fileLog;

        [Docs("Called when the script instance is being loaded.")]
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            fileLog = new FileLog(logFilePath: fileLoggingPath);
            HFLogger.AddLogger(fileLog);
        }

        [Docs("Called on the frame when a script is enabled just before any of the Update methods are called the first time.")]
        private void Start()
        {
            HFLogger.Log(this, $"Start Game from {SceneManager.GetActiveScene().name}");
        }

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

        [Docs("Called when the behaviour becomes disabled or inactive.")]
        private void OnDisable()
        {
            HFLogger.RemoveLogger(fileLog);
        }
    }
}

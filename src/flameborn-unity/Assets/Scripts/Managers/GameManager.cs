using HF.Extensions;
using HF.Logger;
using HF.Logger.FileLogger;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Flameborn.Managers
{
    /// <summary>
    /// This class manages the game lifecycle and logging.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The path where log files are saved.
        /// </summary>
        [SerializeField]
        public readonly string fileLoggingPath = @"C:\Users\gkhan\Documents\Development\Games\flameborn-game\logs\Logs.md";

        /// <summary>
        /// Singleton instance of GameManager.
        /// </summary>
        public static GameManager Instance { get; private set; }

        private bool isInternetConnected;

        /// <summary>
        /// File logger instance.
        /// </summary>
        private FileLog fileLog;


        public bool IsGameRunning { get; private set; } = false;
        private UnityEvent<bool> OnGameRunning { get; set; } = new UnityEvent<bool>();

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            fileLog = new FileLog(logFilePath: fileLoggingPath);
            HFLogger.AddLogger(fileLog);
        }

        /// <summary>
        /// Called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>        
        private void Start()
        {
            HFLogger.Log(this, $"Start Game from {SceneManager.GetActiveScene().name}");
            CheckInternetConnection();

            if (isInternetConnected)
                GameStart();
        }

        public void SubscribeOnGameRunningEvent(UnityAction<bool> onGameRunningEvent)
        {
            OnGameRunning.AddListener(onGameRunningEvent);
            onGameRunningEvent.Invoke(IsGameRunning);
        }

        public void GameStart() { IsGameRunning = true; OnGameRunning.Invoke(IsGameRunning); HFLogger.LogSuccess(this, "Game running."); }
        public void GameStop() { IsGameRunning = false; OnGameRunning.Invoke(IsGameRunning); HFLogger.Log(this, "Game stop."); }

        private void CheckInternetConnection()
        {

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                isInternetConnected = false;
                HFLogger.LogError(this, "No internet connection.");
                UIManager.Instance.AlertController.ShowNetworkError();
                GameStop();
                return;
            }

            isInternetConnected = true;
        }

        /// <summary>
        /// Called when the object becomes enabled and active.
        /// </summary>
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
        private void OnDisable()
        {
            HFLogger.RemoveLogger(fileLog);
        }
    }
}

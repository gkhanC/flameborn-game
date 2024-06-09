using System;
using HF.Extensions;
using HF.Logger;
using HF.Logger.FileLogger;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flameborn.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        public readonly string fileLoggingPath = @"C:\Users\gkhan\Documents\Development\Games\flameborn-game\logs\Logs.md";
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            HFLogger.AddLogger(new FileLog(logFilePath: fileLoggingPath));
        }

        private void Start()
        {
            HFLogger.Log(this, $"Start Game from {SceneManager.GetActiveScene().name}");
        }

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
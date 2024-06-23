// FileLog.cs
using System;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HF.Logger.FileLogger
{
    public class FileLog : ILogType
    {
        private string logFilePath = @"";
        private StreamWriter logFileWriter;

        public FileLog(string logFilePath)
        {
#if UNITY_EDITOR
            this.logFilePath = logFilePath;
#endif
        }

        public void Init()
        {
#if UNITY_EDITOR
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                logFileWriter = new StreamWriter(logFilePath, true);
                WriteInit();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FileLog Init Error]: Could not initialize log file. Exception: {ex.Message}");
            }
#endif
        }

        public void Kill()
        {
#if UNITY_EDITOR
            try
            {
                logFileWriter?.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FileLog Kill Error]: Could not close log file. Exception: {ex.Message}");
            }
#endif
        }

        private void WriteLog(string logLevel, string logObjName, object msg)
        {
#if UNITY_EDITOR
            try
            {
                if (logFileWriter != null)
                {
                    string logMessage = $"[{DateTime.Now.ToShortTimeString()}]" + $"{logLevel}: {logObjName} => {msg}\n";
                    logFileWriter.WriteLine(logMessage);
                    logFileWriter.Flush();
                }
                else
                {
                    Debug.LogError("[FileLog WriteLog Error]: Log file writer is not initialized.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FileLog WriteLog Error]: Could not write to log file. Exception: {ex.Message}");
            }
#endif
        }

        private void WriteInit()
        {
#if UNITY_EDITOR
            try
            {
                if (logFileWriter != null)
                {
                    string logMessage = $"# FileLogger Initialized at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
                    logFileWriter.WriteLine(logMessage);
                    logFileWriter.Flush();
                }
                else
                {
                    Debug.LogError("[FileLog WriteLog Error]: Log file writer is not initialized.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[FileLog WriteLog Error]: Could not write to log file. Exception: {ex.Message}");
            }
#endif
        }

        public void Log(Object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj ? LogObj.name : "NullObject";
            WriteLog("[ LOG ]", name, Msg);
#endif
        }

        public void LogError(Object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj ? LogObj.name : "NullObject";
            WriteLog("[ ERROR ]", name, Msg);
#endif
        }

        public void LogWarning(Object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj ? LogObj.name : "NullObject";
            WriteLog("[ WARNING ]", name, Msg);
#endif
        }

        public void LogSuccess(Object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj ? LogObj.name : "NullObject";
            WriteLog("[ SUCCESS ]", name, Msg);
#endif
        }

        public void LogValidate(Object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj ? LogObj.GetType().ToString() : "Unknown";
            WriteLog("[ VALIDATED ]", name, Msg);
#endif
        }

        public void Log(object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj.GetType().Name;
            WriteLog("[ LOG ]", name, Msg);
#endif
        }

        public void LogError(object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj.GetType().Name;
            WriteLog("[ ERROR ]", name, Msg);
#endif
        }

        public void LogWarning(object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj.GetType().Name;
            WriteLog("[ WARNING ]", name, Msg);
#endif
        }

        public void LogSuccess(object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj.GetType().Name;
            WriteLog("[ SUCCESS ]", name, Msg);
#endif
        }

        public void LogValidate(object LogObj, object Msg)
        {
#if UNITY_EDITOR
            string name = LogObj.GetType().Name;
            WriteLog("[ VALIDATED ]", name, Msg);
#endif
        }
    }
}

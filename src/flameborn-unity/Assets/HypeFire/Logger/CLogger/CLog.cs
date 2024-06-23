using HF.Extensions;
using HF.Logger;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HF.Logger.CLogger
{
    public class CLog : ILogType
    {
        /// <summary>
        /// Logger'ı kullanıma hazırlar.
        /// </summary>
        public void Init()
        {
        }

        /// <summary>
        /// Logger'ı sonlandırır.
        /// </summary>
        public void Kill()
        {
        }

        /// <summary>
        /// Logger default log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void Log(Object LogObj, object Msg)
        {
            string name = LogObj ? LogObj.name : "NullObject";
            Debug.Log(
                $"{"[ LOG ]:>".GetWithColor("gray")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        /// <summary>
        /// Logger LogError log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void LogError(Object LogObj, object Msg)
        {
            string name = LogObj ? LogObj.name : "NullObject";
            Debug.LogError(
                $"{"[ ERROR ]:>".GetWithColor("red")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        /// <summary>
        /// Logger LogWarning log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void LogWarning(Object LogObj, object Msg)
        {
            string name = LogObj ? LogObj.name : "NullObject";
            Debug.LogWarning(
                $"{"[ WARNING ]:>".GetWithColor("yellow")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        /// <summary>
        /// Logger LogSuccess log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void LogSuccess(Object LogObj, object Msg)
        {
            string name = LogObj ? LogObj.name : "NullObject";
            Debug.Log(
                $"{"[ SUCCESS ]:>".GetWithColor("orange")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        // <summary>
        /// Logger LogValidate log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void LogValidate(Object LogObj, object Msg)
        {
            string name = LogObj ? LogObj.GetType().ToString() : "Unknown";
            Debug.Log(
                $"{"[ VALIDATED ]:>".GetWithColor("magenta")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        /// <summary>
        /// Logger default log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen  Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void Log(object LogObj, object Msg)
        {
            string name = LogObj.GetType().Name;
            Debug.Log(
                $"{"[ LOG ]:>".GetWithColor("gray")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        /// <summary>
        /// Logger LogError log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen  Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void LogError(object LogObj, object Msg)
        {
            string name = LogObj.GetType().Name;
            Debug.LogError(
                $"{"[ ERROR ]:>".GetWithColor("red")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        /// <summary>
        /// Logger LogWarning log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen  Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void LogWarning(object LogObj, object Msg)
        {
            string name = LogObj.GetType().Name;
            Debug.LogWarning(
                $"{"[ WARNING ]:>".GetWithColor("yellow")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        /// <summary>
        /// Logger LogSuccess log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void LogSuccess(object LogObj, object Msg)
        {
            string name = LogObj.GetType().Name;
            Debug.Log(
                $"{"[ SUCCESS ]:>".GetWithColor("orange")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }

        /// <summary>
        /// Logger LogValidate log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen  Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        public void LogValidate(object LogObj, object Msg)
        {
            string name = LogObj.GetType().Name;
            Debug.Log(
                $"{"[ VALIDATED ]:>".GetWithColor("magenta")} {name.GetWithColor("lightblue")}{": =>".GetWithColor("green")} {Msg}\n");
        }


        public CLog()
        {
        }
    }
}
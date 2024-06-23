using Object = UnityEngine.Object;

namespace HF.Logger
{
    public interface ILogType
    {
        /// <summary>
        /// Logger'ı kullanıma hazırlar.
        /// </summary>
        void Init();

        /// <summary>
        /// Logger'ı sonlandırır.
        /// </summary>
        void Kill();

        /// <summary>
        /// Logger default log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void Log(Object LogObj, object Msg);

        /// <summary>
        /// Logger LogError log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void LogError(Object LogObj, object Msg);

        /// <summary>
        /// Logger LogWarning log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void LogWarning(Object LogObj, object Msg);

        /// <summary>
        /// Logger LogSuccess log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void LogSuccess(Object LogObj, object Msg);

        /// <summary>
        /// Logger LogValidate log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen nesne</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void LogValidate(Object LogObj, object Msg);

        /// <summary>
        /// Logger default log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen  Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void Log(object LogObj, object Msg);

        /// <summary>
        /// Logger LogError log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen  Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void LogError(object LogObj, object Msg);

        /// <summary>
        /// Logger LogWarning log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen  Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void LogWarning(object LogObj, object Msg);

        /// <summary>
        /// Logger LogSuccess log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void LogSuccess(object LogObj, object Msg);

        /// <summary>
        /// Logger LogValidate log yöntemi.
        /// </summary>
        /// <param name="LogObj">Log sürecini tetikleyen  Nesne/Value</param>
        /// <param name="Msg">Log edilecek mesaj</param>
        void LogValidate(object LogObj, object Msg);
    }
}
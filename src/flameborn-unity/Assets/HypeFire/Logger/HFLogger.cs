using System.Collections.Generic;
using HF.Logger.CLogger;
using UnityEngine.Events;
using Object = UnityEngine.Object;
using System;
using HF.Logger.FileLogger;

namespace HF.Logger
{
    /// <summary>
    /// HFLogger HypeFire Logging konteyneridir.
    /// <para>
    ///     HFLogger ILoggerType interface ile türeliten Logging nesnelerini sakalar ve logging süreçlerini yönetir.
    /// Ayrıca birden fazla Loglama yöntemini eş zamanlı kullanmanıza olanak tanır.
    /// </para>
    /// </summary>
    public static class HFLogger
    {
        /// <summary>
        /// <para>
        ///  CLog HFLogger'ın temel log yöntemidir, UnityEngine Debug modulü üzerinden Log işlemlerini uygular.
        ///  <para>
        ///     CLog sadece editor üzerinde çalışmak üzere, UNITY_EDITOR define ile HFLogger'a implemente edilmiştir. 
        ///  </para>
        /// </para>
        /// </summary>
        private static readonly CLog console_log = new CLog();        

        private static readonly Dictionary<Type, ILogType> _loggers = new Dictionary<Type, ILogType>();

        private static readonly UnityEvent<Object, object> log = new UnityEvent<Object, object>();
        private static readonly UnityEvent<Object, object> log_error = new UnityEvent<Object, object>();
        private static readonly UnityEvent<Object, object> log_warning = new UnityEvent<Object, object>();
        private static readonly UnityEvent<Object, object> log_success = new UnityEvent<Object, object>();
        private static readonly UnityEvent<Object, object> log_validate = new UnityEvent<Object, object>();

        private static readonly UnityEvent<object, object> log_obj = new UnityEvent<object, object>();
        private static readonly UnityEvent<object, object> log_error_obj = new UnityEvent<object, object>();
        private static readonly UnityEvent<object, object> log_warning_obj = new UnityEvent<object, object>();
        private static readonly UnityEvent<object, object> log_success_obj = new UnityEvent<object, object>();
        private static readonly UnityEvent<object, object> log_validate_obj = new UnityEvent<object, object>();

        /// <summary>
        /// HFLogger'da konteyn edilmiş Logger objelere erişir.
        /// </summary>
        public static Dictionary<Type, ILogType> loggers => _loggers;

        /// <summary>
        /// Logger Log eventleri ateşler
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void Log(Object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.Log(LogObj, msg);
#endif
            DoLog(log, LogObj, msg);
        }

        /// <summary>
        /// Logger LogError eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogError(Object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.LogError(LogObj, msg);
#endif
            DoLog(log_error, LogObj, msg);
        }

        /// <summary>
        /// Logger LogSuccess eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogSuccess(Object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.LogSuccess(LogObj, msg);
#endif
            DoLog(log_success, LogObj, msg);
        }


        /// <summary>
        /// Logger LogValidate eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogValidate(Object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.LogValidate(LogObj, msg);
#endif
            DoLog(log_validate, LogObj, msg);
        }

        /// <summary>
        /// Logger LogWarning eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogWarning(Object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.LogWarning(LogObj, msg);
#endif
            DoLog(log_warning, LogObj, msg);
        }

        private static void DoLog(UnityEvent<Object, object> logEvent, Object logObject, object msg)
        {
            logEvent?.Invoke(logObject, msg);
        }


        /// <summary>
        /// Logger Log eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void Log(object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.Log(LogObj, msg);
#endif
            DoLogObj(log_obj, LogObj, msg);
        }

        /// <summary>
        /// Logger LogError eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogError(object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.LogError(LogObj, msg);
#endif
            DoLogObj(log_error_obj, LogObj, msg);
        }

        /// <summary>
        /// Logger LogSuccess eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogSuccess(object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.LogSuccess(LogObj, msg);
#endif
            DoLogObj(log_success_obj, LogObj, msg);
        }

        /// <summary>
        /// Logger LogValidate eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogValidate(object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.LogValidate(LogObj, msg);
#endif
            DoLogObj(log_validate_obj, LogObj, msg);
        }

        /// <summary>
        /// Logger LogWarning eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogWarning(object LogObj, params object[] Msg)
        {
            var msg = String.Join("; ", Msg);
#if UNITY_EDITOR
            console_log.LogWarning(LogObj, msg);
#endif
            DoLogObj(log_warning_obj, LogObj, msg);
        }

        private static void DoLogObj(UnityEvent<object, object> logEvent, object logObject, object msg)
        {
            logEvent?.Invoke(logObject, msg);
        }


        /// <summary>
        /// HFLogger üzeinde parametre ile geçirilen Logger'ı konteyn eder.
        /// <para>
        ///     ILogerType interface ile türetilmiş her Logger type bir kere konteyn edilebilir.
        /// CLog default olarak konteyn edildiğinden tekrar konteyn edilemez.
        /// </para>
        /// </summary>
        /// <param name="logType">ILogType interface ile implemnte edilmiş Logger nesnesi.</param>
        public static void AddLogger(ILogType logType)
        {
            string msg = "";

            if (logType.GetType() == console_log.GetType() || _loggers.ContainsKey(logType.GetType()))
            {
                msg = $"HFLogger has a {logType.GetType()}. You can not add any LogType multiple times.";
                LogWarning(null, msg);
                return;
            }

            _loggers.Add(logType.GetType(), logType);

            log.AddListener(logType.Log);
            log_error.AddListener(logType.LogError);
            log_warning.AddListener(logType.LogWarning);
            log_success.AddListener(logType.LogSuccess);
            log_validate.AddListener(logType.LogValidate);

            logType.Init();

            msg = $"{logType.GetType()} added on HFLogger.";
            LogSuccess(null, msg);
        }

        /// <summary>
        /// Parametre ile geçirilen Logger'ı eğer HFLogger üzerinde konteyn edilmiş ise HFLogger'dan kaldırır.
        /// <para>
        ///     ILogerType interface ile türetilmiş her Logger type bir kere konteyn edilebilir.
        /// CLog default olarak konteyn edildiğinden tekrar konteyn edilemez.
        /// </para>
        /// </summary>
        /// <param name="logType">ILogType interface ile implemnte edilmiş Logger nesnesi.</param>
        public static void RemoveLogger(ILogType logType)
        {
            string msg = "";

            if (!_loggers.ContainsKey(logType.GetType()))
            {
                msg = $"HFLogger has no {logType.GetType()}";
                LogWarning(null, msg);
                return;
            }

            _loggers.Remove(logType.GetType());

            log.RemoveListener(logType.Log);
            log_error.RemoveListener(logType.LogError);
            log_warning.RemoveListener(logType.LogWarning);
            log_success.RemoveListener(logType.LogSuccess);
            log_validate.RemoveListener(logType.LogValidate);

            logType.Kill();

            msg = $"{logType.GetType()} removed on HFLogger.";
            LogSuccess(null, msg);
        }
    }
}
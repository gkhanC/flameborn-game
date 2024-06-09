namespace HF.Logger
{
    using Object = UnityEngine.Object;

    public static class HFLoggerExtensions
    {
        /// <summary>
        /// Logger Log eventleri ateşler
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void Log(this Object mObj, params object[] msg)
        {
            HFLogger.Log(mObj, msg);
        }

        /// <summary>
        /// Logger LogError eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogError(this Object mObj, params object[] msg)
        {
            HFLogger.LogError(mObj, msg);
        }

        /// <summary>
        /// Logger LogWarning eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogWarning(this Object mObj, params object[] msg)
        {
            HFLogger.LogWarning(mObj, msg);
        }

        /// <summary>
        /// Logger LogSuccess eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogSuccess(this Object mObj, params object[] msg)
        {
            HFLogger.LogSuccess(mObj, msg);
        }

        /// <summary>
        /// Logger LogValidate eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Unity nesnesi.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogValidate(this Object mObj, params object[] msg)
        {
            HFLogger.LogValidate(mObj, msg);
        }

        /// <summary>
        /// Logger Log eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="mObj">Log edilecek mesaj.</param>
        public static void Log(this Object mObj)
        {
            HFLogger.Log(mObj, mObj);
        }

        /// <summary>
        /// Logger Log eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void Log(this object mObj, params object[] msg)
        {
            HFLogger.Log(mObj, msg);
        }

        /// <summary>
        /// Logger LogError eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogError(this object mObj, params object[] msg)
        {
            HFLogger.LogError(mObj, msg);
        }

        /// <summary>
        /// Logger LogWarning eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogWarning(this object mObj, params object[] msg)
        {
            HFLogger.LogWarning(mObj, msg);
        }

        /// <summary>
        /// Logger LogSuccess eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        public static void LogSuccess(this object mObj, params object[] msg)
        {
            HFLogger.LogSuccess(mObj, msg);
        }

        /// <summary>
        /// Logger LogValidate eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="Msg">Log edilecek mesaj.</param>
        public static void LogValidate(this object mObj, params object[] msg)
        {
            HFLogger.LogValidate(mObj, msg);
        }

        /// <summary>
        /// Logger Log eventlerini ateşler.
        /// <para>
        ///     CLog ve eğer varsa konteyn edilmiiş Logging yöntemleri ile Loglama yapar.
        /// </para>
        /// </summary>
        /// <param name="LogObj">Logging'i ateşleyen Nesne/Value.</param>
        /// <param name="mObj">Log edilecek mesaj.</param>
        public static void Log(this object mObj)
        {
            HFLogger.Log(mObj, mObj);
        }
    }
}
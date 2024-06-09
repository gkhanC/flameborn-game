namespace HF.EditorUtilities.Folder
{
    /// <summary>
    /// Folder helper arayüzü.
    /// </summary>
    public interface IFolderHelper
    {
        /// <summary>
        /// Yeni dosya yaratır.
        /// </summary>
        /// <returns></returns>
        bool Create();

        /// <summary>
        /// Mevcut dosyayı siler.
        /// </summary>
        /// <returns></returns>
        bool Delete();

        /// <summary>
        /// Helper'da belirtilen dosya ve path'in varlığını doğrular.
        /// </summary>
        /// <param name="isLoggerEnable"></param>
        /// <returns></returns>
        bool IsValidate(bool isLoggerEnable = true);
    }
}
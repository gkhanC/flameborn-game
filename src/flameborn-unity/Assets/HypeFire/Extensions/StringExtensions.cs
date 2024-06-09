namespace HF.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// String'e Xml tagi kullanarak color value ekler,
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="colorName">hex kodu veya xml color name</param>
        /// <returns></returns>
        public static string GetWithColor(this string Value, string colorName)
        {
            return $"<color={colorName}>{Value}</color>";
        }
    }
}
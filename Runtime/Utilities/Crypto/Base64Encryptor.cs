using System;
using System.Text;

namespace EasySaveUnityService.Utilities.Crypto
{
    /// <summary>
    /// Класс для работы с шифрованием Base64
    /// </summary>
    internal static class Base64Encryptor
    {
        /// <summary>
        /// Получает UTF8 строку из base64
        /// </summary>
        /// <param name="_encodedString">строка в base64</param>
        /// <returns>строка в UTF8</returns>
        public static string DecodeBase64ToUTF8(string _encodedString)
        {
            var bytes = Convert.FromBase64String(_encodedString);
            var decodedString = Encoding.UTF8.GetString(bytes);
            return decodedString;
        }
        
        /// <summary>
        /// Получает base64 строку из UTF8
        /// </summary>
        /// <param name="_originalString">строка в UTF8</param>
        /// <returns>строка в base64</returns>
        public static string EncodeUTF8ToBase64(string _originalString)
        {
            var bytes = Encoding.UTF8.GetBytes(_originalString);
            var encodedString = Convert.ToBase64String(bytes);
            return encodedString;
        }
    }
}
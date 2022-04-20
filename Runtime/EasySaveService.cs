using System.IO;
using EasySaveUnityService.Utilities.Crypto;
using UnityEngine;

namespace EasySaveUnityService
{
    /// <summary>
    /// Основной класс сервиса.
    /// Включает в себя все публичные методы, необходимые пользователю для сохранения/загрузки данных
    /// </summary>
    public static class EasySaveService
    {
        /// <summary>
        /// Путь до корневой папки кэша
        /// </summary>
        private static string rootFolderPath => Application.persistentDataPath;
        
        /// <summary>
        /// Сохраняет данные объекта в файл
        /// </summary>
        /// <param name="_objectToSave">Объект, который нужно сохранить</param>
        /// <param name="_fileName">Имя файла</param>
        /// <param name="_isNeedEncryption">Нужно ли закодировать файл перед сохранением</param>
        public static void SaveData(object _objectToSave, string _fileName, bool _isNeedEncryption = false)
        {
            var filePath = GetFilePath(_fileName);

            var fileString = JsonUtility.ToJson(_objectToSave);
            if (_isNeedEncryption)
                fileString = EncryptString(fileString);
            
            File.WriteAllText(filePath, fileString);
        }

        /// <summary>
        /// Получает объект класса T из файла. Если не удалось - возвращает null
        /// </summary>
        /// <param name="_fileName">Имя файла</param>
        /// <param name="_isNeedDecryption">Нужно ли расшифровывать файл</param>
        /// <typeparam name="T">Тип объекта. Должен быть классом</typeparam>
        /// <returns>Десериализованный объект класса T</returns>
        public static T LoadData<T>(string _fileName, bool _isNeedDecryption = false) where T : class
        {
            var filePath = GetFilePath(_fileName);
            if (!File.Exists(filePath))
                return null;

            var fileString = File.ReadAllText(filePath);
            if (_isNeedDecryption)
                fileString = DecryptString(fileString);
            
            return JsonUtility.FromJson<T>(fileString);
        }
        
        /// <summary>
        /// Шифрует строку.
        /// Пока что только в Base64 todo
        /// </summary>
        /// <param name="_stringToEncrypt">Исходная строка</param>
        /// <returns>Зашифрованная строка</returns>
        private static string EncryptString(string _stringToEncrypt)
            => Base64Encryptor.EncodeUTF8ToBase64(_stringToEncrypt);

        /// <summary>
        /// Расшифровывает строку.
        /// Пока что только из Base64 todo
        /// </summary>
        /// <param name="_stringToDecrypt">Зашифрованная строка</param>
        /// <returns>Расшифрованная строка</returns>
        private static string DecryptString(string _stringToDecrypt)
            => Base64Encryptor.DecodeBase64ToUTF8(_stringToDecrypt);
        
        /// <summary>
        /// Получает полный путь до файла по его имени
        /// </summary>
        /// <param name="_fileName">Имя файла</param>
        /// <returns>Полный путь до файла</returns>
        private static string GetFilePath(string _fileName) => Path.Combine(rootFolderPath, _fileName);
    }
}
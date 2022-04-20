using System;
using Newtonsoft.Json;

namespace SaveExample.Scripts
{
    /// <summary>
    /// Пример класса для сохранения/загрузки
    /// </summary>
    [Serializable]
    public class SaveDataExample
    {
        /// <summary>
        /// Имя игрока
        /// Сохраняется в файл как "name"
        /// </summary>
        [JsonProperty("name")]
        public string m_playerName;
        /// <summary>
        /// Биограбия игрока
        /// Сохраняется в файл как "bio"
        /// </summary>
        [JsonProperty("bio")]
        public string m_playerBio;
        /// <summary>
        /// Кол-во жизней игрока
        /// Сохраняется в файл как "health"
        /// </summary>
        [JsonProperty("health")]
        public int m_playerHealth;
        /// <summary>
        /// Список названий оружия
        /// Сохраняется в файл как "gunsNames"
        /// </summary>
        [JsonProperty("gunsNames")]
        public string[] m_playerGunsNames;
    }
}
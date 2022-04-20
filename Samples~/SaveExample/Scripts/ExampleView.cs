using System.IO;
using EasySaveUnityService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SaveExample.Scripts
{
    /// <summary>
    /// Контроллер интерфейса примера
    /// </summary>
    public class ExampleView : MonoBehaviour
    {
        [Header("Inputs")]
        [SerializeField] private TMP_InputField m_fileNameInput;
        [SerializeField] private TMP_InputField m_playerNameInput;
        [SerializeField] private TMP_InputField m_playerBioInput;
        [SerializeField] private TMP_InputField m_playerHealthInput;
        [SerializeField] private TMP_InputField m_playerGunsInput;
        [Header("Controls")]
        [SerializeField] private Button m_saveButton;
        [SerializeField] private Button m_loadButton;
        [SerializeField] private Toggle m_encryptionToggle;
        
        private void Awake()
        {
            m_saveButton.onClick.AddListener(SaveButtonAction);
            m_loadButton.onClick.AddListener(LoadButtonAction);
        }

        /// <summary>
        /// Действие кнопки сохранения
        /// </summary>
        private void SaveButtonAction()
        {
            var data = GetUiData();
            
            EasySaveService.SaveData(data, m_fileNameInput.text, m_encryptionToggle.isOn);
            
            Debug.Log($"File saved in [{Path.Combine(Application.persistentDataPath, m_fileNameInput.text)}]");
        }

        /// <summary>
        /// Действие кнопки загрузки
        /// </summary>
        private void LoadButtonAction()
        {
            var data = EasySaveService.LoadData<SaveDataExample>(m_fileNameInput.text, m_encryptionToggle.isOn);
            
            if (data == null)
                Debug.LogError("file not exist");
            
            FillUi(data);
        }

        /// <summary>
        /// Заполнение интерфейса из загруженного объекта
        /// </summary>
        /// <param name="_data">Загруженный из файла объект</param>
        private void FillUi(SaveDataExample _data)
        {
            m_playerNameInput.SetTextWithoutNotify(_data?.m_playerName ?? "file not exist");
            m_playerBioInput.SetTextWithoutNotify(_data?.m_playerBio ?? "file not exist");
            m_playerHealthInput.SetTextWithoutNotify(_data?.m_playerHealth.ToString() ?? "file not exist");
            m_playerGunsInput.SetTextWithoutNotify(string.Join(", ", _data?.m_playerGunsNames ?? new [] {"file not exist"}));
        }

        /// <summary>
        /// Создание сохраняемого объекта с помощью данных в интерфейсе 
        /// </summary>
        /// <returns>Объект для сохранения</returns>
        private SaveDataExample GetUiData()
        {
            if (!int.TryParse(m_playerHealthInput.text, out var health))
                health = -1;

            var guns = m_playerGunsInput.text.Split(',');

            return new SaveDataExample
            {
                m_playerName = m_playerNameInput.text,
                m_playerBio = m_playerBioInput.text,
                m_playerHealth = health,
                m_playerGunsNames = guns,
            };
        }
    }
}
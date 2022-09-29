using NHD.DataController.Common;
using NHD.StaticData.Settings;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class SettingsDataLoader : MonoBehaviour, IDataLoader
    {
        private string PATH;

        private void Awake()
        {
            PATH = $"{Application.persistentDataPath}/SettingsData.json";
            LoadData();
        }

        public void LoadData()
        {
            FileCheck();

            FileStream fs = new FileStream(PATH, FileMode.Open);
            byte[] data = new byte[fs.Length];

            fs.Read(data, 0, data.Length);
            fs.Close();

            string jsonData = Encoding.UTF8.GetString(data);
            SettingsJsonConstruct settingsJsonData = JsonUtility.FromJson<SettingsJsonConstruct>(jsonData);

            SetupData(settingsJsonData);
        }

        private void FileCheck()
        {
            if (!File.Exists(PATH))
            {
                FileStream fs = new FileStream(PATH, FileMode.Create);
                byte[] data = ReturnByteCodeInitialData();
                fs.Write(data, 0, data.Length);
                fs.Close();

                return;
            }

            return;
        }

        private byte[] ReturnByteCodeInitialData()
        {
            FileStream fs = new FileStream($"{Application.dataPath}/Resources/StaticData/InitialData/INITIAL_SETTINGS_DATA.json", FileMode.Open);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            return data;
        }

        private void SetupData(SettingsJsonConstruct settingsJsonData)
        {
            StaticSettingsData._languageIndex = settingsJsonData.language_index;
            StaticSettingsData._resolutionIndex = settingsJsonData.resolution_index;
            StaticSettingsData._bgmVolume = settingsJsonData.bgm_volume;
            StaticSettingsData._effectVolume = settingsJsonData.effect_volume;
            StaticSettingsData._isAutoSave = settingsJsonData.is_auto_save;
            StaticSettingsData._isEasyMode = settingsJsonData.is_easy_mode;
        }
    }
}
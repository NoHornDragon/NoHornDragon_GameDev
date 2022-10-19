using NHD.DataController.Common;
using NHD.StaticData.Settings;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class SettingsDataLoader : IDataLoader
    {
        private static string PATH;

        public void SetupData()
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

            ApplyStaticData(settingsJsonData);
        }

        private void FileCheck()
        {
            if (!File.Exists(PATH))
            {
                InitializeData();
                return;
            }

            return;
        }

        public void InitializeData()
        {
            FileStream fs = new FileStream(PATH, FileMode.Create);
            byte[] data = ReturnByteCodeInitialData();
            fs.Write(data, 0, data.Length);
            fs.Close();
        }

        private byte[] ReturnByteCodeInitialData()
        {
            FileStream fs = new FileStream($"{Application.dataPath}/StreamingAssets/StaticData/InitialData/INITIAL_SETTINGS_DATA.json", FileMode.Open);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            return data;
        }

        private void ApplyStaticData(SettingsJsonConstruct settingsJsonData)
        {
            StaticSettingsData._languageIndex = settingsJsonData.language_index;
            StaticSettingsData._resolutionIndex = settingsJsonData.resolution_index;
            StaticSettingsData._bgmVolume = settingsJsonData.bgm_volume;
            StaticSettingsData._effectVolume = settingsJsonData.effect_volume;
            StaticSettingsData._isAutoSave = settingsJsonData.is_auto_save;
            StaticSettingsData._isHardMode = settingsJsonData.is_hard_mode;
        }
    }
}
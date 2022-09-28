using NHD.DataController.Common;
using NHD.StaticData.Settings;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Savers
{
    public class SettingsDataSaver : MonoBehaviour, IDataSaver
    {
        private string PATH;
        private SettingsJsonConstruct settingsJsonData = new SettingsJsonConstruct();

        private void Awake()
        {
            PATH = $"{Application.persistentDataPath}/SettingsData.json";
        }

        private void GetData()
        {
            settingsJsonData.language_index = StaticSettingsData._languageIndex;
            settingsJsonData.resolution_index = StaticSettingsData._resolutionIndex;
            settingsJsonData.bgm_volume = StaticSettingsData._bgmVolume;
            settingsJsonData.effect_volume = StaticSettingsData._effectVolume;
            settingsJsonData.is_auto_save = StaticSettingsData._isAutoSave;
            settingsJsonData.is_easy_mode = StaticSettingsData._isEasyMode;
        }

        public void SaveData()
        {
            GetData();
            string jsonData = JsonUtility.ToJson(settingsJsonData);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            FileStream fs = new FileStream(PATH, FileMode.Create);

            fs.Write(data, 0, data.Length);
            fs.Close();
        }
    }
}
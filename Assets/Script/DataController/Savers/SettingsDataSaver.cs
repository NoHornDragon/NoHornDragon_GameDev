using NHD.StaticData.Settings;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Savers
{
    public class SettingsDataSaver
    {
        private static string PATH = $"{Application.persistentDataPath}/SettingsData.json";
        private static SettingsJsonConstruct _settingsJsonData = new SettingsJsonConstruct();

        private static void GetData()
        {
            _settingsJsonData.language_index = StaticSettingsData._languageIndex;
            _settingsJsonData.resolution_index = StaticSettingsData._resolutionIndex;
            _settingsJsonData.bgm_volume = StaticSettingsData._bgmVolume;
            _settingsJsonData.effect_volume = StaticSettingsData._effectVolume;
            //_settingsJsonData.is_auto_save = StaticSettingsData._isAutoSave;
            _settingsJsonData.is_hard_mode = StaticSettingsData._isHardMode;
        }

        public static void SaveData()
        {
            GetData();
            string jsonData = JsonUtility.ToJson(_settingsJsonData);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            FileStream fs = new FileStream(PATH, FileMode.Create);

            fs.Write(data, 0, data.Length);
            fs.Close();
        }
    }
}
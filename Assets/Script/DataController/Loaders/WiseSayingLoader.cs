using Newtonsoft.Json;
using NHD.StaticData.Settings;
using NHD.StaticData.WiseSaying;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class WiseSayingLoader
    {
        private static string PATH;

        public static void SetupData()
		{
            CheckLanguage();
            LoadData();
		}

        private static void CheckLanguage()
        {
            switch (StaticSettingsData._languageIndex)
            {
                case 0:
                    PATH = $"{Application.dataPath}/StreamingAssets/StaticData/WiseSaying/STATIC_WISESAYING_DATA_KOR.json";
                    break;
                case 1:
                    PATH = $"{Application.dataPath}/StreamingAssets/StaticData/WiseSaying/STATIC_WISESAYING_DATA_ENG.json";
                    break;
            }
        }

        private static void LoadData()
        {
            FileStream fs = new FileStream(PATH, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string stringData = sr.ReadToEnd();

            fs.Close();
            sr.Close();
            StaticWiseSayingData._staticWiseSayingData = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringData);
        }
    }
}

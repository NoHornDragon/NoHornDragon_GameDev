using Newtonsoft.Json;
using NHD.StaticData.Settings;
using NHD.StaticData.UIString;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class UIStringLoader
    {
        private static string PATH;
        public static Action LanguageChangeAction;

        public static void SetupData()
        {
            CheckLanguage();
            LoadData();
            LanguageChangeAction();
        }

        private static void CheckLanguage()
        {
            switch (StaticSettingsData._languageIndex)
            {
                case 0:
                    PATH = $"{Application.dataPath}/StreamingAssets/StaticData/UIStringData/STATIC_UI_STRING_KOR.json";
                    break;
                case 1:
                    PATH = $"{Application.dataPath}/StreamingAssets/StaticData/UIStringData/STATIC_UI_STRING_ENG.json";
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
            StaticUIString._staticUIString = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringData);
        }
    }
}
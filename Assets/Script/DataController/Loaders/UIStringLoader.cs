using Newtonsoft.Json;
using NHD.DataController.Common;
using NHD.StaticData.Settings;
using NHD.StaticData.UIString;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class UIStringLoader : MonoBehaviour, IDataLoader
    {
        private string PATH;
        private Encoding encode;

        private void Awake()
        {
            Debug.Log(StaticSettingsData._languageIndex);
            CheckLanguage();
            LoadData();
        }

        private void CheckLanguage()
        {
            switch (StaticSettingsData._languageIndex)
            {
                case 0:
                    encode = Encoding.GetEncoding("ks_c_5601-1987");
                    PATH = $"{Application.dataPath}/Resources/StaticData/UIStringData/STATIC_UI_STRING_KOR.json";
                    break;
                case 1:
                    encode = Encoding.UTF8;
                    PATH = $"{Application.dataPath}/Resources/StaticData/UIStringData/STATIC_UI_STRING_ENG.json";
                    break;
            }
        }

        public void LoadData()
        {
            FileStream fs = new FileStream(PATH, FileMode.Open);
            StreamReader sr = new StreamReader(fs, encode);
            string stringData = sr.ReadToEnd();

            fs.Close();
            sr.Close();
            StaticUIString._staticUIString = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringData);
        }
    }
}
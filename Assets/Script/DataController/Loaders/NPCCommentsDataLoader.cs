using Newtonsoft.Json;
using NHD.DataController.Common;
using NHD.StaticData.NPCComment;
using NHD.StaticData.Settings;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class NPCCommentsDataLoader
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
                    PATH = $"{Application.dataPath}/Resources/StaticData/NPCCommentsData/STATIC_NPC_COMMENTS_DATA_KOR.json";
                    break;
                case 1:
                    PATH = $"{Application.dataPath}/Resources/StaticData/NPCCommentsData/STATIC_NPC_COMMENTS_DATA_ENG.json";
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
            StaticNPCCommentsData._staticNPCCommentsData = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringData);
        }
    }
}

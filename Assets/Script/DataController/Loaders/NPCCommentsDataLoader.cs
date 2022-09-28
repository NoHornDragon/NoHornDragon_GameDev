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
    public class NPCCommentsDataLoader : MonoBehaviour, IDataLoader
    {
        private string PATH;

        private void Awake()
        {
            CheckLanguage();
            LoadData();
        }

        private void CheckLanguage()
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

        public void LoadData()
        {
            FileStream fs = new FileStream(PATH, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string stringData = sr.ReadToEnd();

            fs.Close();
            sr.Close();
            StaticNPCCommentData._staticNPCCommentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringData);
        }
    }
}

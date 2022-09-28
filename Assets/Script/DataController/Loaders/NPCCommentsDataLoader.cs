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
        private Encoding encode;

        private void Awake()
        {
            Debug.Log(StaticSettingsData._languageIndex);
            CheckLanguage();
            LoadData();
            Debug.Log(StaticNPCCommentData._staticNPCCommentData["MINER_02"]);
        }

        private void CheckLanguage()
        {
            switch (StaticSettingsData._languageIndex)
            {
                case 0:
                    encode = Encoding.GetEncoding("ks_c_5601-1987");
                    PATH = $"{Application.dataPath}/Resources/StaticData/NPCCommentsData/STATIC_NPC_COMMENTS_DATA_KOR.json";
                    break;
                case 1:
                    encode = Encoding.UTF8;
                    PATH = $"{Application.dataPath}/Resources/StaticData/NPCCommentsData/STATIC_NPC_COMMENTS_DATA_ENG.json";
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
            StaticNPCCommentData._staticNPCCommentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringData);
        }
    }
}

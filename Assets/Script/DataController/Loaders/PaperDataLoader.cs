using NHD.DataController.Common;
using NHD.StaticData.History;
using NHD.StaticData.Settings;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class PaperDataLoader
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
                    PATH = $"{Application.dataPath}/StreamingAssets/StaticData/PaperData/STATIC_PAPER_DATA_KOR.json";
                    break;
                case 1:
                    PATH = $"{Application.dataPath}/StreamingAssets/StaticData/PaperData/STATIC_PAPER_DATA_ENG.json";
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

            PaperJsonConstruct paperData = JsonUtility.FromJson<PaperJsonConstruct>(stringData);
            StaticHistoryData._nodes = paperData.paper_nodes;
        }
    }
}
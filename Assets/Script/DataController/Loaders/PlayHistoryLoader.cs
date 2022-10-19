using NHD.DataController.Common;
using NHD.StaticData.History;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class PlayHistoryLoader : IDataLoader
    {
        private static string PATH;

        public void SetupData()
        {
            PATH = $"{Application.persistentDataPath}/PlayHistoryData.json";
            FileCheck();
            LoadData();
        }

        public void LoadData()
        {
            FileStream fs = new FileStream(PATH, FileMode.Open);
            byte[] data = new byte[fs.Length];

            fs.Read(data, 0, data.Length);
            fs.Close();

            string jsonData = Encoding.UTF8.GetString(data);
            PlayHistoryJsonConstruct playHistoryJsonData = JsonUtility.FromJson<PlayHistoryJsonConstruct>(jsonData);

            ApplyStaticData(playHistoryJsonData);
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
            FileStream fs = new FileStream($"{Application.dataPath}/StreamingAssets/StaticData/InitialData/INITIAL_PLAY_HISTORY_DATA.json", FileMode.Open);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            return data;
        }

        private void ApplyStaticData(PlayHistoryJsonConstruct playHistoryJsonData)
        {
            StaticHistoryData._isGetPapers = playHistoryJsonData.is_get_papers;
            StaticHistoryData._totalPlayTime = playHistoryJsonData.total_play_time;
            StaticHistoryData._restartCount = playHistoryJsonData.restart_count;
            StaticHistoryData._stunCount = playHistoryJsonData.stun_count;
        }
    }
}
using NHD.DataController.Common;
using NHD.StaticData.History;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Loaders
{
    public class PlayHistoryLoader : MonoBehaviour, IDataLoader
    {
        private string PATH;

        private void Awake()
        {
            PATH = $"{Application.persistentDataPath}/PlayHistoryData.json";
            LoadData();
        }

        public void LoadData()
        {
            FileCheck();

            FileStream fs = new FileStream(PATH, FileMode.Open);
            byte[] data = new byte[fs.Length];

            fs.Read(data, 0, data.Length);
            fs.Close();

            string jsonData = Encoding.UTF8.GetString(data);
            PlayHistoryJsonConstruct playHistoryJsonData = JsonUtility.FromJson<PlayHistoryJsonConstruct>(jsonData);

            SetupData(playHistoryJsonData);
        }

        private void FileCheck()
        {
            if (!File.Exists(PATH))
            {
                FileStream fs = new FileStream(PATH, FileMode.Create);
                byte[] data = ReturnByteCodeInitialData();
                fs.Write(data, 0, data.Length);
                fs.Close();

                return;
            }

            return;
        }

        private byte[] ReturnByteCodeInitialData()
        {
            FileStream fs = new FileStream($"{Application.dataPath}/Resources/StaticData/InitialData/INITIAL_PLAY_HISTORY_DATA.json", FileMode.Open);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();

            return data;
        }

        private void SetupData(PlayHistoryJsonConstruct playHistoryJsonData)
        {
            StaticHistoryData._totlaPlayTime = playHistoryJsonData.total_play_time;
            StaticHistoryData._restartCount = playHistoryJsonData.restart_count;
            StaticHistoryData._stunCount = playHistoryJsonData.stun_count;
        }
    }
}
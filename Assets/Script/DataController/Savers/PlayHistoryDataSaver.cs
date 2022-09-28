using NHD.DataController.Common;
using NHD.StaticData.History;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Savers
{
    public class PlayHistoryDataSaver : MonoBehaviour, IDataSaver
    {
        private string PATH;
        private PlayHistoryJsonConstruct playHistoryJsonData = new PlayHistoryJsonConstruct();

        private void Awake()
        {
            PATH = $"{Application.persistentDataPath}/PlayHistoryData.json";
        }

        private void GetData()
        {
            playHistoryJsonData.total_play_time = StaticHistoryData._totlaPlayTime;
            playHistoryJsonData.restart_count = StaticHistoryData._restartCount;
            playHistoryJsonData.stun_count = StaticHistoryData._stunCount;
        }

        public void SaveData()
        {
            GetData();
            string jsonData = JsonUtility.ToJson(playHistoryJsonData);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            FileStream fs = new FileStream(PATH, FileMode.Create);

            fs.Write(data, 0, data.Length);
            fs.Close();
        }
    }
}
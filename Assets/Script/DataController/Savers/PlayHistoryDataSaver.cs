using NHD.StaticData.History;
using System.IO;
using System.Text;
using UnityEngine;

namespace NHD.DataController.Savers
{
    public class PlayHistoryDataSaver
    {
        private static string PATH = $"{Application.persistentDataPath}/PlayHistoryData.json";
        private static PlayHistoryJsonConstruct _playHistoryJsonData = new PlayHistoryJsonConstruct();

        private static void GetData()
        {
            _playHistoryJsonData.is_get_papers = StaticHistoryData._isGetPapers;
            _playHistoryJsonData.total_play_time = StaticHistoryData._totalPlayTime;
            _playHistoryJsonData.restart_count = StaticHistoryData._restartCount;
            _playHistoryJsonData.stun_count = StaticHistoryData._stunCount;
        }

        public static void SaveData()
        {
            GetData();
            string jsonData = JsonUtility.ToJson(_playHistoryJsonData);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            FileStream fs = new FileStream(PATH, FileMode.Create);

            fs.Write(data, 0, data.Length);
            fs.Close();
        }
    }
}
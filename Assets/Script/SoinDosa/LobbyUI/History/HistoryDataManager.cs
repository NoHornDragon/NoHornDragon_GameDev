using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class HistoryDataManager : MonoBehaviour
{
    public static HistoryDataManager instance;
    [SerializeField]
    private HistoryData historyData;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        LoadHistoryData();
    }

    public HistoryData GetHistoryData()
    {
        return historyData;
    }
    public void SetPaperTrue(int _val)
    {
        historyData.activeNodes[_val] = true;
    }
    public void AddPlayTime(int _val)
    {
        historyData.playTime += _val;
    }
    public void AddStunCount(int _val)
    {
        historyData.stunCount += _val;
    }
    public void AddRestartCount(int _val)
    {
        historyData.restartCount += _val;
    }
    public void SaveHistoryData()
    {
        string jsonData = JsonUtility.ToJson(historyData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        FileStream fs = new FileStream(Application.persistentDataPath + "/HistoryData.json", FileMode.Create);
        fs.Write(data, 0, data.Length);
        fs.Close();
    }
    public void LoadHistoryData()
    {
        if (!File.Exists(Application.persistentDataPath + "/HistoryData.json"))
        {
            Debug.Log("No HistoryData!");
            SaveHistoryData();
            return;
        }
        FileStream fs = new FileStream(Application.persistentDataPath + "/HistoryData.json", FileMode.Open);

        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        HistoryData saveData = JsonUtility.FromJson<HistoryData>(jsonData);
        historyData = saveData;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    [Header("저장데이터")]
    public UserData userData;


    #region Save & Load
    [ContextMenu("New Game")]
    void NewGame()
    {
        // 아직은 예시
        userData = new UserData();
        SaveGame();
    }

    [ContextMenu("Save Game")]
    public void SaveGame()
    {
        // Debug.Log("Game Save");
        
        string dataToJson = JsonUtility.ToJson(userData, true);

        // byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dataToJson);
        // string code = System.Convert.ToBase64String(bytes);

        string path = Path.Combine(Application.dataPath, "UserDate.json");
        // File.WriteAllText(path, code);

        // 암호화시 아래 삭제
        File.WriteAllText(path, dataToJson);
    }

    [ContextMenu("Load Game")]
    public void LoadGame()
    {
        // Debug.Log("Load Game");
        string path = Path.Combine(Application.dataPath, "UserDate.json");

        if(!File.Exists(path))
        {
            // TODO : 이곳에 저장 없는 예외처리
            NewGame();
            return;
        }
        string DataFromJson = File.ReadAllText(path);

        // byte[] bytes = System.Convert.FromBase64String(DataFromJson);
        // string classData = System.Text.Encoding.UTF8.GetString(bytes);
        // saveData = JsonUtility.FromJson<SaveData>(classData);

        // 암호화 하면 아래 삭제
        userData = JsonUtility.FromJson<UserData>(DataFromJson);
    }

    #endregion
}


[System.Serializable]
public class UserData{
    public float backGroundsoundVolume;
    public float effectSoundVolume;
    public uint clearCount;
    public uint resetCount;
    public bool[] paperList;

    public UserData(){
        backGroundsoundVolume = 0.5f;
        effectSoundVolume = 0.5f;
    }
};
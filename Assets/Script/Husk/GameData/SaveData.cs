using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // TODO
        // LoadGame();
    }

    
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

        string path = Path.Combine(Application.dataPath, "UserData.json");
        // File.WriteAllText(path, code);

        // 암호화시 아래 삭제
        File.WriteAllText(path, dataToJson);
    }

    [ContextMenu("Load Game")]
    public void LoadGame()
    {
        // Debug.Log("Load Game");
        string path = Path.Combine(Application.dataPath, "UserData.json");

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
    [Header("사운드 설정")]
    public float backGroundsoundVolume;     // 인게임 내 BGM 볼륨
    public float effectSoundVolume;         // 인게임 내 사운드 볼륨
    
    [Header("플레이어 기록")]
    public uint clearCount;                 // 플레이어의 게임 엔딩 본 횟수
    public uint resetCount;                 // 플레이어가 R 버튼을 눌러 리셋한 횟수
    public uint stunedCount;                // 플레이어가 스턴을 당한 횟수

    [Header("플레이어 자동저장 여부")]
    [SerializeField] private bool easyMode; // 플레이어의 저장 기능
    public bool UseEasyMode
    {
        get
        {
            return easyMode;
        }
        set
        {
            easyMode = value;
            if(!easyMode)
                playerPos = Vector3.zero;
        }
    }
    [SerializeField] private Vector3 playerPos;               // 저장을 했을 때 플레이어 위치
    public Vector3 PlayerPos 
    { 
        get
        {
            return playerPos;
        } 
        set 
        {
            playerPos = value;
        }
    }

    [Header("종이조각 흭득 여부")]
    public bool[] paperList;                // 플레이어의 종이조각 갯수

    public UserData()
    {
        backGroundsoundVolume = 0.5f;
        effectSoundVolume = 0.5f;

        clearCount = 0;
        resetCount = 0;
        stunedCount = 0;

        easyMode = false;
        playerPos = Vector3.zero;

        paperList = new bool[20];
    }
};
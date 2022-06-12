using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPCTalks
{
    [Header("방문 횟수")]
    public int count;
    [Header("talk[0] = kor, talk[1] = eng")]
    [TextArea]
    public string[] talk;
}
[System.Serializable]
public class NPCTalksData
{
    public List<NPCTalks> npcTalksList;
}
public class NPC : MonoBehaviour
{
    [Header("대사 파일 이름")]
    public string filePath;

    private Coroutine crtPtr;

    [Header("플레이어 인식 거리")]
    public float catchDistance;
    [Header("플레이어 멀리갔음을 인식하는 거리")]
    public float farDistance;

    [Header("그 외")]
    [SerializeField]
    private GameObject visitText; // NPC를 방문하면 출력되는 텍스트
    [SerializeField]
    private GameObject textBox; // 텍스트 박스
    
    [Header("방문 횟수가 이상일 경우 출력하는 텍스트")]
    //public NPCTalks[] npcTalks;
    //[Header("Test")]
    public NPCTalksData npcTalks;
    private BoxCollider2D npcCol;

    public int visitCount; // NPC를 방문한 횟수

    public BoxCollider2D farCheckCol; // NPC에게서 멀어짐을 체크하는 함수
    private void Awake()
    {
        npcCol = this.GetComponent<BoxCollider2D>();
        farCheckCol = this.transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();

        if (!File.Exists(Application.dataPath + "/Resources/NpcTexts/" + filePath + ".json"))
        {
            Debug.Log(filePath + " load failed");
        }
        else
        {
            FileStream fs = new FileStream(string.Format("{0}/Resources/NpcTexts/{1}.json", Application.dataPath, filePath), FileMode.Open);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, data.Length);
            fs.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            npcTalks = JsonUtility.FromJson<NPCTalksData>(jsonData);
            Debug.Log(jsonData);
        }
    }
    private void Start()
    {
        SetColSize();
        SetFarColSize();
    }

    private void SetColSize()
    {
        npcCol.size = new Vector2(catchDistance, catchDistance);
    }
    private void SetFarColSize()
    {
        farCheckCol.size = new Vector2(farDistance, farDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            textBox.SetActive(true);
            visitText.SetActive(true);
            crtPtr = StartCoroutine(TalkCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(crtPtr != null)
                StopCoroutine(crtPtr);
            visitText.GetComponent<TextMesh>().text = "";
            visitText.SetActive(false);
            textBox.SetActive(false);
        }
    }

    IEnumerator TalkCoroutine()
    {
        for (int i = npcTalks.npcTalksList.Count - 1; i >= 0; i--)
        {
            if (visitCount >= npcTalks.npcTalksList[i].count)
            {
                for(int j = 0; j < npcTalks.npcTalksList[i].talk[SettingsManager.instance.languageDropdown.value].Length; j++)
                {
                    visitText.GetComponent<TextMesh>().text = npcTalks.npcTalksList[i].talk[SettingsManager.instance.languageDropdown.value].Substring(0, j);
                    yield return new WaitForSeconds(0.05f);
                }
                visitText.GetComponent<TextMesh>().text = npcTalks.npcTalksList[i].talk[SettingsManager.instance.languageDropdown.value];
                break;
            }
        }
    }

}

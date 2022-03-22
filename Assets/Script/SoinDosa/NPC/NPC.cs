using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class NPCTalks
{
    public NPCTalks(int _count, string _talk)
    {
        count = _count;
        talk = _talk;
    }
    public int count;
    public string talk;
}
public class NPC : MonoBehaviour
{
    [Header("플레이어 인식 거리")]
    public float catchDistance;
    [Header("플레이어 멀리갔음을 인식하는 거리")]
    public float farDistance;

    [Header("그 외")]
    public GameObject visitText; // NPC를 방문하면 출력되는 텍스트
    [Header("방문 횟수가 이상일 경우 출력하는 텍스트")]
    public NPCTalks[] npcTalks;

    private BoxCollider2D npcCol;

    public int visitCount; // NPC를 방문한 횟수

    public BoxCollider2D farCheckCol; // NPC에게서 멀어짐을 체크하는 함수
    private void Awake()
    {
        npcCol = this.GetComponent<BoxCollider2D>();
        farCheckCol = this.transform.GetChild(0).GetComponentInChildren<BoxCollider2D>();
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
            visitText.SetActive(true);

            for (int i = npcTalks.Length - 1; i >= 0; i--)
            {
                if(visitCount >= npcTalks[i].count)
                {
                    visitText.GetComponent<TextMesh>().text = npcTalks[i].talk;
                    break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            visitText.SetActive(false);
        }
    }


}

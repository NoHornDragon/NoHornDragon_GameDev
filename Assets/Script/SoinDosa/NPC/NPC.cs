using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Header("플레이어 인식 거리")]
    public float catchDistance;
    [Header("플레이어 멀리갔음을 인식하는 거리")]
    public float farDistance;

    [Header("그 외")]
    public GameObject visitText; // NPC를 방문하면 출력되는 텍스트 <- 이걸 나중에 따로 매니저로 빼야함
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
        visitText = GameObject.Find("Canvas").transform.Find("Visit_Text").gameObject;
    }
    private void SetFarColSize()
    {
        farCheckCol.size = new Vector2(farDistance, farDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            visitText.GetComponent<Text>().text = "안녕하신가 여행자여! " + visitCount + "번 방문했구만!";
            visitText.SetActive(true);
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

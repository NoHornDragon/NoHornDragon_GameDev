using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class WiseSayingTeller : MonoBehaviour
{

    List<string> tempWiseSaying = new List<string>{"1번 멍뭉이", "2번 고양이", "3번\n 줄바꿈"};
    [SerializeField] private GameObject wiseSayingUI;
    [SerializeField] private Image bg;
    [SerializeField] private TextMeshProUGUI wiseSayingText;
    Color32 bgcolor;
    void Start()
    {
        wiseSayingUI.SetActive(false);

        
    }

    [ContextMenu("명언 UI ON")]
    private void ShowWiseSayingOnScreen()
    {
        wiseSayingUI.SetActive(true);

    }

}

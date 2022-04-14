﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
public class WiseSayingTeller : MonoBehaviour
{   
    [System.Serializable]
    private class WiseSaying{
        public string[] sayings;
        // [languageNo][saying Index]
    }
    [SerializeField] WiseSaying wiseSayingClass;
    [SerializeField] private GameObject wiseSayingUI;
    [SerializeField] private TextMeshProUGUI wiseSayingText;
    void Start()
    {
        wiseSayingUI.SetActive(false);
        wiseSayingClass.sayings = new string[] 
        { "당신은 방금 당신의 노력이 아무 짝에도 쓸모없다는 소중한 사실을 깨달았습니다.",
        "시작이 반의 반의 반이다",
        "당신이 생각한 지름길은 오르막길이 아니라 내리막길 이었나 봅니다",
        "웃으면 안 되지만 자꾸 입 꼬리가 올라가며 웃음이 나는 군요",
        "가장 위대한 행보는 가장 낮아지는 것이다",
        "당신이 느끼는 감정이 저희가 게임을 만든 의도입니다",
        "영원히 떨어질 것처럼 꿈꾸고 오늘 올라갈 것처럼 떨어져라",
        "현명한 판단은 경험에서 나오고, 경험은 그릇된 로그아웃에서 나온다",
        "열정은 현재의 위치와 같다는데 당신의 열정은 바닥났나 보네요",
        "머리가 나쁘면 몸이... -올라가기 힘겨워 가실 때에는 말없이 고이 보내 드리오리다",
        "시시한 비극은 추락하는 것이지요",
        "‘생각보다 어렵다’ 라는 말은 그래도 ‘생각’이라는 걸 했다는 것인데 당신은... -혹시 발에 쇳덩이를 다신 건가요?",
        "처음은 모두 아름답습니다. 올해의 첫 눈도, 당신의 첫 추락도.",
        "운영자를 보고 싶으시겠지만 인내의 미덕을 키우시는 것이 더 생산적일 겁니다.",
        "넘어질 때 마다 일어나는 오뚝이조차도 올라가는 법은 배우지 못 했습니다.",
        "욕하시는 것은 아니죠? 오는 말이 고와야 가는 말도 곱습니다.",
        "백지장을 맞들어도 나아지는 것은 없을 겁니다. -그대의 상처는 우리의 성공입니다. "};
        
    }

    [ContextMenu("명언 UI ON")]
    private void ShowWiseSayingOnScreen()
    {

        // Have a appear tweening wiseSayingUI's OnEnable()
        // And will disappear automatically
        wiseSayingText.text = wiseSayingClass.sayings[Random.Range(0, wiseSayingClass.sayings.Length)];
        wiseSayingUI.SetActive(true);

    }

    [ContextMenu("저장")]
    public void SaveSaying()
    {
        // Debug.Log("Game Save");
        
        string dataToJson = JsonUtility.ToJson(wiseSayingClass, true);

        string path = Path.Combine(Application.dataPath, "WiseSaying.json");

        File.WriteAllText(path, dataToJson);
    }
    [ContextMenu("로드")]
    public void Load()
    {
        string path = Path.Combine(Application.dataPath, "WiseSaying.json");

        string DataFromJson = File.ReadAllText(path);

        wiseSayingClass = JsonUtility.FromJson<WiseSaying>(DataFromJson);
    }

}
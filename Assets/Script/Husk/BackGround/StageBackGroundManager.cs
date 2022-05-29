using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBackGroundManager : MonoBehaviour
{
    [SerializeField] private uint stageIndex;
    private bool nowActive = true;
    [SerializeField] Transform[] bgImages;

    private void Start()
    {
        bgImages = gameObject.GetComponentsInChildren<Transform>();
    
        PlayerEnterConfiner[] confiners = FindObjectsOfType<PlayerEnterConfiner>();
        foreach(PlayerEnterConfiner confiner in confiners)
        {
            confiner.ActiveRoomEvent += StageEvent;
        }

        ActiveBG(false);
    }

    private void StageEvent(uint playerStage, bool isIn)
    {
        /*
        현재 액티브가 아닌데 플레이어가 스테이지에 들어온 경우, 액티브인데 스테이지를 벗어난 경우 함수를 호출합니다.
        그 외의 경우 SetActive 작업을 할 필요가 없습니다.
        */

        if (playerStage != stageIndex) return;

        ActiveBG(isIn);
    }

    private void ActiveBG(bool isActive)
    {
        if (nowActive == isActive) return;

        for(int i = 1; i < bgImages.Length; i++)
        {
            bgImages[i].gameObject.SetActive(isActive);
        }

        nowActive = isActive;
    }
}                                           
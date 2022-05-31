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

    /// <summary>
    /// 활성화된 스테이지와 플레이어의 현재 스테이지가 맞지 않는 경우 배경 처리를 합니다.
    /// </summary>
    private void StageEvent(uint playerStage, bool isIn)
    {
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
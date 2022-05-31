using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stageList;


    /// <param name="inputStageNumber">스테이지 번호</param>
    /// <param name="isIn">플레이어가 들어오면 true, 아니면 false</param>
    public void StageChange(uint inputStageNumber, bool isIn)
    {
        if(isIn)
        {
            stageList[inputStageNumber - 1].SetActive(true);
            return;
        }

        stageList[inputStageNumber - 1].SetActive(false);
    }
}


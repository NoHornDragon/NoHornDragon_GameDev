using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stageList;
    private uint stageNumber;

    private void Awake()
    {
        //for(int i = 0; i < stageList.Length; i++)
        //{
        //    stageList[i].SetActive(false);
        //}
    }


    /// <param name="inputStageNumber">스테이지 번호</param>
    /// <param name="isIn">플레이어가 들어오면 true, 아니면 false</param>
    public void StageChange(uint inputStageNumber, bool isIn)
    {
        if(isIn)
        {
            stageNumber = inputStageNumber;
            return;
        }

        if (inputStageNumber == stageNumber) return;

        if (inputStageNumber > 1)
            stageList[inputStageNumber - 2].SetActive(true);
        if (inputStageNumber < stageList.Length)
            stageList[inputStageNumber].SetActive(true);

        if (inputStageNumber >= 3)
            stageList[inputStageNumber - 3].SetActive(false);
        if (inputStageNumber + 1 < stageList.Length)
            stageList[inputStageNumber + 1].SetActive(false);
    }
}
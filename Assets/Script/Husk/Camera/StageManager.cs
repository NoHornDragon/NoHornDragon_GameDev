using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stageList;
    [SerializeField] private uint nowStageNumber;

 
    public void StageChange(uint inputStageNumber, bool isIn)
    {
        if(isIn)
        {
            stageList[inputStageNumber - 1].SetActive(true);
            this.nowStageNumber = inputStageNumber;
            return;
        }

        stageList[inputStageNumber - 1].SetActive(false);
        // if (nowStageNumber == inputStageNumber) return;
    }
}


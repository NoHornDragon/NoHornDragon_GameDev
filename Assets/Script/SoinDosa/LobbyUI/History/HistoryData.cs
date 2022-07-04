using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HistoryData
{
    public HistoryData(bool[] _activeNodes, int _playTime, float _minPlayTime, int _stunCount, int _restartCount)
    {
        activeNodes = _activeNodes;
        playTime = _playTime;
        minPlayTime = _minPlayTime;
        stunCount = _stunCount;
        restartCount = _restartCount;
    }
    public bool[] activeNodes;
    public int playTime;
    public float minPlayTime;
    public int stunCount;
    public int restartCount;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTimer : MonoBehaviour
{
    [SerializeField]
    private float playTime = 0.0f;
    private bool timerActive = true;

    private void Start()
    {
        FindObjectOfType<MenuButtonManager>().menuButtonEvent += SetTimerStatue;
    }

    public void SetTimerStatue(bool isActive)
    {
        timerActive = isActive;
    }

    public float GetPlayTime()
    {
        return playTime;
    }

    private void Update()
    {
        if(!timerActive)    return;

        playTime += Time.deltaTime;
    }

}

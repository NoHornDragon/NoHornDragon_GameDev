using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayTimer : MonoBehaviour
{
    [SerializeField]
    private float playTime = 0.0f;
    private bool timerActive = true;
    
    [SerializeField]
    private Material seasonMaterial;

    private void Start()
    {
        FindObjectOfType<MenuButtonManager>().menuButtonEvent += SetTimerStatue;

        seasonMaterial = FindObjectOfType<ParallaxSprite>().GetComponent<Material>();
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

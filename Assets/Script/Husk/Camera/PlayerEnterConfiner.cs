using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEnterConfiner : MonoBehaviour
{
    public event Action<uint, bool> ActiveRoomEvent;
    // [SerializeField] private float lensSize;
    [SerializeField] private uint stageIndex;
    private void Start()
    {
        ActiveRoomEvent += FindObjectOfType<BackGroundScroller>().test;
        ActiveRoomEvent += FindObjectOfType<StageManager>().StageChange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Debug.Log($"stage {stageIndex} - Enter");
            ActiveRoomEvent?.Invoke(stageIndex, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Debug.Log($"stage {stageIndex} - Exit");
            ActiveRoomEvent?.Invoke(stageIndex, false);
        }
    }
}

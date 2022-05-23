using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEnterConfiner : MonoBehaviour
{
    public event Action<uint, bool, uint> ActiveRoomEvent;
    private PolygonCollider2D polygonCollider2D;
    // [SerializeField] private float lensSize;
    [SerializeField] private uint roomNo;
    [SerializeField] private uint stageIndex;
    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();

        ActiveRoomEvent += FindObjectOfType<RoomManager>().RoomChange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log($"room {roomNo} - Enter");
            ActiveRoomEvent?.Invoke(roomNo, true, stageIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log($"room {roomNo} - Exit");
            ActiveRoomEvent?.Invoke(roomNo, false, stageIndex);
        }
    }
}

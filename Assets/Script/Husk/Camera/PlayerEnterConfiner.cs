using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEnterConfiner : MonoBehaviour
{
    public event Action<uint, PolygonCollider2D, float, bool> ActiveRoomEvent;
    private PolygonCollider2D polygonCollider2D;
    [SerializeField] private float lensSize;
    [SerializeField] private uint roomNo;
    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();

        ActiveRoomEvent += FindObjectOfType<RoomManager>().RoomChange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ActiveRoomEvent?.Invoke(roomNo, polygonCollider2D, lensSize, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ActiveRoomEvent?.Invoke(roomNo, polygonCollider2D, lensSize, false);
        }
    }
}

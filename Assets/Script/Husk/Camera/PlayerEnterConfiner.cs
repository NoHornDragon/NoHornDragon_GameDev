using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEnterConfiner : MonoBehaviour
{
    public event Action<uint> ActiveRoomEvent;
    private PolygonCollider2D polygonCollider2D;
    [SerializeField] private float lensSize;
    [SerializeField] private uint roomNo;
    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();

        ActiveRoomEvent += FindObjectOfType<RoomManager>().EnterRoom;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<CameraManager>().ChangeCamera(polygonCollider2D, lensSize);

            if(ActiveRoomEvent != null)
                ActiveRoomEvent(roomNo);
        }
    }
}

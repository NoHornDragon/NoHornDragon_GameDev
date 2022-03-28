using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private GameObject[] roomList;

    private void Start()
    {
        roomList = GameObject.FindGameObjectsWithTag("Room");
    }

    public void EnterRoom(uint inputRoomNo, PolygonCollider2D roomConfiner, float lensSize)
    {
        //! warn : real room unmber start with 1
        roomList[inputRoomNo-1].SetActive(true);

        // out of range exception
        // Deactive room list => distance 2 from current room
        if(inputRoomNo > 2)
            roomList[inputRoomNo-3].SetActive(false);

        if(inputRoomNo > roomList.Length + 2)
            roomList[inputRoomNo+1].SetActive(false);

        FindObjectOfType<CameraManager>().ChangeCamera(roomConfiner, lensSize);
    }
}

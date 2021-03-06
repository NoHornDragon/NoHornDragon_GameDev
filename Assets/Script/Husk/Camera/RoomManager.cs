using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/*
This class managing room tilemap(object) and camera.
In `RoomChange` function's `isIn` parameter means called from 'ontriggerenter', 'ontriggerexit'

If function triggered by 'enter' this will just remember info.
And finally triggered by 'exit' this will change actual room by `CameraChangeEvent` (this is in CameraManager().ChangeCamera)

Seperate remembering, changing camera prevent below bug.
- if player stay in two room's border(prev, new) and go to previous room, camera still rendering new room.

And if change room, this class active only 3 rooms(previous, current, next). and make other room's active false
To doing this initialize roomstages active false
*/

public class RoomManager : MonoBehaviour
{
    // public event Action<PolygonCollider2D, float> CameraChangeEvent;
    [SerializeField] private GameObject[] roomList;
    [SerializeField] private uint roomNumber;
    // TODO : 테스트 후 제거
    public bool nowTesting;
    // private PolygonCollider2D confiner = null;
    // private float cameraSize;
    [Header("스테이지")]
    [SerializeField] private GameObject[] stageList;
    private uint stageNumber;
    
    // private void Start()
    // {
    //     roomList = GameObject.FindGameObjectsWithTag("Room");

    //     if(nowTesting)  return;

    //     for(int i = 2; i < roomList.Length; i++)
    //     {
    //         roomList[i].SetActive(false);
    //     }
    // }

    public void RoomChange(uint inputStageNumber, bool isIn)
    {
        if(nowTesting)  return;
        // if(confiner == null)
        // {
        //     // this means initial state
        //     this.roomNumber = inputRoomNo;
        //     this.confiner = roomConfiner;
        //     this.cameraSize = lensSize;
        //     // CameraChangeEvent?.Invoke(roomConfiner, lensSize);
        //     return;
        // }
        if(isIn)
        {
            this.stageNumber = inputStageNumber;
            return;
        }

        if(inputStageNumber == stageNumber)   return;

        // Deactive room list => distance 2 from current room
        // should active [-2], [-1(current)], [0]
        // if(roomNumber > 1)
        //     roomList[roomNumber-2].SetActive(true);
        // if(roomNumber < roomList.Length)
        //     roomList[roomNumber].SetActive(true);
        
        // // deactive [-3]. [+1] room
        // if(roomNumber >= 3)
        //     roomList[roomNumber-3].SetActive(false);
        // if(roomNumber + 1 < roomList.Length)
        //     roomList[roomNumber+1].SetActive(false);


        // finally change camera
        // CameraChangeEvent?.Invoke(confiner, cameraSize);
    }
}



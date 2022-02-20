using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomManager : MonoBehaviour
{
    /*
    this clas is...
    목표 : 플레이어가 있는 방을 기준으로 1칸 범위의 방들을 활성화
    
    - 총 활성화 되어야 하는 방의 수는 3개 (이후 기획이 직선 방향이 아니게된다면 변경)
    - 

    만약 3,4,5 활성화가 되어 있고
    
    */
    public List<GameObject> roomQ = new List<GameObject>();
    public Dictionary<uint, GameObject> roomTable = new Dictionary<uint, GameObject>();


    public void EnterRoom(GameObject room, uint inputRoomNo)
    {
        if(!roomTable.ContainsKey(inputRoomNo))
            roomTable.Add(inputRoomNo, room);

        if(roomTable.Count < 4)
            return;
    }

    public void ExitRoom()
    {

    }
}

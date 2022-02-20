using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterConfiner : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;
    [SerializeField] private float lensSize;
    [SerializeField] private uint roomNo;
    [SerializeField] private GameObject room;
    private void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<CameraManager>().ChangeCamera(polygonCollider2D, lensSize);
            //TODO : 이거 실제 방으로 오브젝트 변경
            FindObjectOfType<RoomManager>().EnterRoom(this.gameObject, roomNo);
        }
    }
}

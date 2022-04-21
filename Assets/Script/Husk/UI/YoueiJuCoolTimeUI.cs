using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YoueiJuCoolTimeUI : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private Vector3 UIOffset;
    private Camera cam;
    void Start()
    {   
        if(SaveData.instance.userData.UseEasyMode)  Destroy(gameObject);

        playerPos = transform.parent.parent;
        cam = Camera.main;
    }

    void Update()
    {
        transform.position = cam.WorldToScreenPoint(playerPos.position + UIOffset);
    }
}

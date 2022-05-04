using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDFollowPlayer : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private Vector3 UIOffset;
    private Camera cam;
    void Start()
    {   
        // if using easy mode, game don't need this hud
        if(SaveData.instance.userData.UseEasyMode)  Destroy(gameObject);

        playerPos = transform.parent.parent;
        cam = Camera.main;
    }

    void Update()
    {
        // following player
        transform.position = cam.WorldToScreenPoint(playerPos.position + UIOffset);
    }
}

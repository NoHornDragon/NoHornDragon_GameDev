using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SingleCameraMovement : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private CinemachineConfiner confiner;
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner>();
        
        cam.Follow = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

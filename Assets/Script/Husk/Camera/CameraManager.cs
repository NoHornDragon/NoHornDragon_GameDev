using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camera1;
    [SerializeField] CinemachineVirtualCamera camera2;
    CinemachineConfiner camera1Confiner;
    CinemachineConfiner camera2Confiner;
    private bool isCam1;

    void Start()
    {
        camera1Confiner = camera1.GetComponent<CinemachineConfiner>();
        camera2Confiner = camera2.GetComponent<CinemachineConfiner>();

        // make cinemachine follow player
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        SetCameraFollow(camera1, player);
        SetCameraFollow(camera2, player);
        
    }

    public void ChangeCamera(PolygonCollider2D border, float lensSize)
    {
        if(isCam1)
        {
            camera2Confiner.m_BoundingShape2D = border;
            camera2.m_Lens.OrthographicSize = lensSize;

            camera1.gameObject.SetActive(false);
            camera2.gameObject.SetActive(true);
            isCam1 = false;

        }
        else 
        {
            camera1Confiner.m_BoundingShape2D = border;
            camera1.m_Lens.OrthographicSize = lensSize;

            camera2.gameObject.SetActive(false);
            camera1.gameObject.SetActive(true);
            isCam1 = true;
        }
    }

    private void SetCameraFollow(CinemachineVirtualCamera camera, Transform target)
    {
        camera.Follow = target;
    }
}

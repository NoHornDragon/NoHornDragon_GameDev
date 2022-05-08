// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Cinemachine;

// public class CameraManager : MonoBehaviour
// {
//     [SerializeField] private CinemachineVirtualCamera camera1;
//     [SerializeField] private CinemachineVirtualCamera camera2;
//     private CinemachineConfiner camera1Confiner;
//     private CinemachineConfiner camera2Confiner;
//     private bool isCam1;

//     void Start()
//     {
//         camera1Confiner = camera1.GetComponent<CinemachineConfiner>();
//         camera2Confiner = camera2.GetComponent<CinemachineConfiner>();

//         FindObjectOfType<RoomManager>().CameraChangeEvent += ChangeCamera;

//         // make cinemachine follow player
//         Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
//         SetCameraFollow(camera1, player);
//         SetCameraFollow(camera2, player);
//     }

//     public void ChangeCamera(PolygonCollider2D border, float lensSize)
//     {
//         if(isCam1)
//         {
//             DoChangeCamera(camera2, camera2Confiner, border, lensSize);
//             return;
//         }
        
//         DoChangeCamera(camera1, camera1Confiner, border, lensSize);
//     }

//     private void DoChangeCamera(CinemachineVirtualCamera cam, CinemachineConfiner confiner, PolygonCollider2D border, float lensSize)
//     {
//         confiner.m_BoundingShape2D = border;
//         cam.m_Lens.OrthographicSize = lensSize;

//         // if camera1 is active(isCam1 is true) -> camera1(false), camera2(true)
//         // if cmaera2 is active(isCam1 is false) -> camera1(true), cmaera2(false)
//         camera1.gameObject.SetActive(!isCam1);
//         camera2.gameObject.SetActive(isCam1);

//         isCam1 = !(isCam1);
//     }

//     private void SetCameraFollow(CinemachineVirtualCamera camera, Transform target)
//     {
//         camera.Follow = target;
//     }
// }

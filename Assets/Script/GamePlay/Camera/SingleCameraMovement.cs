using Cinemachine;
using NHD.Entity.Player;
using UnityEngine;

namespace NHD.GamePlay.Camera
{
    public class SingleCameraMovement : MonoBehaviour
    {
        [Header("카메라 Zoom")]
        [SerializeField] private float maxZoomTime;
        public float zoomTimer;
        [SerializeField] private float zoomCoolTime;
        public float coolTimer;
        [SerializeField] private float zoomSpeed;
        [SerializeField] private float maxCameraSize;
        public float initialLensSize;
        public bool initialized = false;

        private CinemachineVirtualCamera cam;
        private CinemachineConfiner confiner;

        private void Start()
        {
            cam = GetComponent<CinemachineVirtualCamera>();
            confiner = GetComponent<CinemachineConfiner>();

            cam.Follow = FindObjectOfType<PlayerMovement>().gameObject.transform.GetChild(3);
            initialLensSize = cam.m_Lens.OrthographicSize;
        }


        private void Update()
        {

            if (Input.GetKey(KeyCode.LeftShift) && coolTimer < 0)
            {
                zoomCamera();
                return;
            }

            if (coolTimer > 0)
                coolTimer -= Time.deltaTime;

            if (initialized) return;
            SetCameraInitial();
        }

        private void zoomCamera()
        {
            if (zoomTimer > maxZoomTime)
            {
                SetCameraInitial();
                return;
            }

            initialized = false;

            // zoom out camera
            if (cam.m_Lens.OrthographicSize < maxCameraSize)
                cam.m_Lens.OrthographicSize += Time.deltaTime * zoomSpeed;

            zoomTimer += Time.deltaTime;
        }

        private void SetCameraInitial()
        {
            coolTimer = zoomCoolTime;

            zoomTimer = 0;
            cam.m_Lens.OrthographicSize = initialLensSize;

            initialized = true;
        }
    }
}
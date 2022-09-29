using UnityEngine;

namespace NHD.Multiplay.ServerSide
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager _instance;
        public GameObject _playerPrefab;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Debug.Log($"instance is already exist");
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;

            Server.Start(8, 26950);
        }

        private void OnApplicationQuit()
        {
            Server.Stop();
        }

        public PlayerTrackerInServer InstantiatePlayer()
        {
            return Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerTrackerInServer>();
        }
    }
}
using UnityEngine;

namespace NHD.Multiplay.ServerSide
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager instance;

        public GameObject playerPrefab;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
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

        public Player InstantiatePlayer()
        {
            return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        }
    }
}
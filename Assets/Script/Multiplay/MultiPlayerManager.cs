using NHD.Multiplay.ClientSide;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.Multiplay
{
    public class MultiPlayerManager : MonoBehaviour
    {
        public static MultiPlayerManager instance;
        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

        public GameObject localPlayerPrefab;
        public GameObject playerPrefab;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log($"indtance already exist, so destroy objec!");
                Destroy(this);
            }
        }

        public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation)
        {
            GameObject _player;
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(localPlayerPrefab, _position, _rotation);
            }
            else
            {
                _player = Instantiate(playerPrefab, _position, _rotation);
            }

            _player.GetComponent<PlayerManager>().id = _id;
            _player.GetComponent<PlayerManager>().username = _username;
            players.Add(_id, _player.GetComponent<PlayerManager>());
        }
    }
}
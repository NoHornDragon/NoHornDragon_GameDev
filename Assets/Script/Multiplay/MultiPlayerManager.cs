using NHD.Multiplay.ClientSide;
using System.Collections.Generic;
using UnityEngine;

using NHD.UI.EmojiUI;

namespace NHD.Multiplay
{
    public class MultiPlayerManager : MonoBehaviour
    {
        public static MultiPlayerManager _instance;
        // ClientHandle에서 이 Dictionary를 활용해 각종 정보 처리를 합니다.
        public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

        public GameObject _localPlayerPrefab;
        public GameObject _playerPrefab;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Debug.Log($"indtance already exist, so destroy objec!");
                Destroy(this);
            }
        }

        public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation)
        {
            GameObject _player;
            if (id == Client.instance.myId)
            {
                _player = Instantiate(_localPlayerPrefab, position, rotation);
                FindObjectOfType<EmojiUIManager>().SetEmojiUITarget(_player.transform.GetChild(0));
                FindObjectOfType<EmojiUIManager>()._serverEmojiEvent += _player.transform.GetChild(0).GetComponent<LocalPlayerController>().SendEmojiInfoToServer;
            }
            else
            {
                _player = Instantiate(_playerPrefab, position, rotation);
            }

            _player.GetComponent<PlayerManager>().id = id;
            _player.GetComponent<PlayerManager>().username = username;
            players.Add(id, _player.GetComponent<PlayerManager>());
        }
    }
}
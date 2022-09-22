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
        public static Dictionary<int, MultiPlayPlayerInfo> _players = new Dictionary<int, MultiPlayPlayerInfo>();

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
            GameObject player;
            if (id == Client._instance._myId)
            {
                player = Instantiate(_localPlayerPrefab, position, rotation);
                FindObjectOfType<EmojiUIManager>().SetEmojiUITarget(player.transform.GetChild(0));
                FindObjectOfType<EmojiUIManager>()._serverEmojiEvent += player.transform.GetChild(0).GetComponent<LocalPlayerController>().SendEmojiInfoToServer;
            }
            else
            {
                player = Instantiate(_playerPrefab, position, rotation);
            }

            player.GetComponent<MultiPlayPlayerInfo>()._id = id;
            player.GetComponent<MultiPlayPlayerInfo>()._username = username;
            _players.Add(id, player.GetComponent<MultiPlayPlayerInfo>());
        }
    }
}
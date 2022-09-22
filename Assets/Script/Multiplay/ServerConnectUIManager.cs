using NHD.Multiplay.ClientSide;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.Multiplay
{
    public class ServerConnectUIManager : MonoBehaviour
    {
        public static ServerConnectUIManager _instance;

        public GameObject _startMenu;
        public InputField _userNameField;
        public InputField _ipAddressField;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Debug.Log($"indtance already exist, so destroy object!");
                Destroy(this);
            }
        }

        public void ConnectToServer(bool isHost)
        {
            _startMenu.SetActive(false);
            _userNameField.interactable = false;

            if (!isHost)
            {
                Debug.Log($"Client build!");
                _ipAddressField.interactable = false;
                Client._instance._ip = _ipAddressField.text;
            }

            Client._instance.ConnectToServer();
        }
    }
}
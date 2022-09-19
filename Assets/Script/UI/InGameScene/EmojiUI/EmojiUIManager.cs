using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.EmojiUI
{
    public class EmojiUIManager : MonoBehaviour
    {
        private int _emojiIndexToPopup = -1;
        private int _curEmojiIndex = -1;
        public int _pingIndex { set{ _emojiIndexToPopup = value; } }
        // should target to PingUI in inspector
        [SerializeField] private GameObject _pingInterface;
        // Should target to player in inspector
        [SerializeField] private Transform _targetToPopup;
        [SerializeField] private EmojiSpawner _emojiSpawner;

        private void Update()
        {
            if(Input.GetMouseButtonDown(2))
                SetPingUIActive(true);
            if(Input.GetMouseButtonUp(2))
                SetPingUIActive(false);                
        }

        private void SetPingUIActive(bool isActive)
        {
            _pingInterface.SetActive(isActive);

            if(!isActive)
                PopupEmoji();
        }

        public void PopupEmoji()
        {
            if(_emojiIndexToPopup == -1)  return;

            _emojiSpawner.SpawnEmoji(_emojiIndexToPopup, _targetToPopup);

            _emojiIndexToPopup = -1;
        }
    }
}


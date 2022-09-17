using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.EmojiUI
{
    public class EmojiUIManager : MonoBehaviour
    {
        public List<EmojiInGame> _emojiList;

        private bool _mouseButtonDown;
        [SerializeField] private int _emojiIndexToPopup = -1;
        public int _pingIndex { set{ _emojiIndexToPopup = value; } }
        // should target to PingUI in inspector
        [SerializeField] private GameObject _pingInterface;
        // Should target to player in inspector
        [SerializeField] private Transform _targetToPopup;
        [SerializeField] private Vector2 _emojiInGameOffset;

        private void Update()
        {
            if(Input.GetMouseButtonDown(2))
                SetPingUI(true);
            if(Input.GetMouseButtonUp(2))
                SetPingUI(false);                
        }

        /// <summary>
        /// Get ping index from ui element
        /// </summary>
        public void PopupEmoji()
        {
            if(_emojiIndexToPopup == -1)  return;

            // TODO : 여기서 인덱스를 받아 whereToTaret 에 처리
            Debug.Log($"{_emojiIndexToPopup}");

            var emoji = Instantiate(_emojiList[_emojiIndexToPopup]);
            emoji.transform.SetParent(_targetToPopup);
            emoji.transform.localPosition = _emojiInGameOffset;

            _emojiIndexToPopup = -1;
        }


        private void SetPingUI(bool isActive)
        {
            _pingInterface.SetActive(isActive);

            if(!isActive)
                PopupEmoji();
        }
    }
}


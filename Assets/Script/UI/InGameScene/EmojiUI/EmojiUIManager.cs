using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.EmojiUI
{
    public class EmojiUIManager : MonoBehaviour
    {
        public List<EmojiInGame> _emojiList;

        private bool _mouseButtonDown;
        private int _emojiIndexToPopup = -1;
        private int _curEmojiIndex = -1;
        public int _pingIndex { set{ _emojiIndexToPopup = value; } }
        // should target to PingUI in inspector
        [SerializeField] private GameObject _pingInterface;
        // Should target to player in inspector
        [SerializeField] private Transform _targetToPopup;
        [SerializeField] private Transform _emojiPool;
        [SerializeField] private Vector2 _emojiInGameOffset;

        private void Start()
        {
            SetEmojiReturnEvent();
        }

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
            
            ReturnEmojiObjectToPool();

            // Active and Popup emoji 
            var emoji = _emojiList[_emojiIndexToPopup];
            emoji.transform.SetParent(_targetToPopup);
            emoji.transform.localPosition = _emojiInGameOffset;
            emoji.gameObject.SetActive(true);

            _curEmojiIndex = _emojiIndexToPopup;
            _emojiIndexToPopup = -1;
        }

        private void ReturnEmojiObjectToPool()
        {
            if(_curEmojiIndex == -1)    return;
            
            _emojiList[_curEmojiIndex].transform.SetParent(_emojiPool);
            _emojiList[_curEmojiIndex].gameObject.SetActive(false);
            _curEmojiIndex = -1;
        }


        private void SetPingUI(bool isActive)
        {
            _pingInterface.SetActive(isActive);

            if(!isActive)
                PopupEmoji();
        }

        private void SetEmojiReturnEvent()
        {
            for(int i = 0; i < _emojiList.Count; i++)
            {
                _emojiList[i]._returnCallbackEvent += ReturnEmojiObjectToPool;
            }
        }
    }
}


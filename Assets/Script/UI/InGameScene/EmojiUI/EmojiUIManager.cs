using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.EmojiUI
{
    public class EmojiUIManager : MonoBehaviour
    {
        public List<EmojiInGame> _emojiList;
        private int _emojiIndexToPopup = -1;
        private int _curEmojiIndex = -1;
        public int _pingIndex { set{ _emojiIndexToPopup = value; } }
        // should target to PingUI in inspector
        [SerializeField] private GameObject _pingInterface;
        // Should target to player in inspector
        [SerializeField] private Transform _targetToPopup;
        [SerializeField] private Transform _emojiPool;
        [SerializeField] private Vector2 _emojiOffsetInGame;

        private void Start()
        {
            SetEmojiReturnEvent();
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(2))
                SetPingUIActive(true);
            if(Input.GetMouseButtonUp(2))
                SetPingUIActive(false);                
        }

        /// <summary>
        /// Get ping index from ui element
        /// </summary>
        public void PopupEmoji()
        {
            if(_emojiIndexToPopup == -1)  return;
            
            ReturnEmojiObjectToPool();

            SetEmojiState(_emojiList[_emojiIndexToPopup], true);

            _curEmojiIndex = _emojiIndexToPopup;
            _emojiIndexToPopup = -1;
        }

        private void ReturnEmojiObjectToPool()
        {
            if(_curEmojiIndex == -1)    return;

            SetEmojiState(_emojiList[_curEmojiIndex], false);
            _curEmojiIndex = -1;
        }


        private void SetEmojiState(EmojiInGame emoji, bool isActive)
        {
            if(isActive)
            {
                emoji.transform.SetParent(_targetToPopup);
                emoji.transform.localPosition = _emojiOffsetInGame;
                emoji.gameObject.SetActive(true);
                return;
            }

            emoji.transform.SetParent(_emojiPool);
            emoji.gameObject.SetActive(false);
        }

        private void SetPingUIActive(bool isActive)
        {
            _pingInterface.SetActive(isActive);

            if(!isActive)
                PopupEmoji();
        }

        private void SetEmojiReturnEvent()
        {
            for(int i = 0; i < _emojiList.Count; i++)
            {
                _emojiList[i]._returnToPoolCallbackEvent += ReturnEmojiObjectToPool;
            }
        }
    }
}


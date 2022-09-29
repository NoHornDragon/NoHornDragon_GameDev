using UnityEngine;

namespace NHD.UI.EmojiUI
{
    public class EmojiUIButton : MonoBehaviour
    {
        [SerializeField] private int _pingIconIndex;
        private EmojiUIManager _pingUIManager;

        private void Start()
        {
            _pingUIManager = transform.parent.parent.GetComponent<EmojiUIManager>();
        }

        public void SetEmojiIndex()
        {
            _pingUIManager._pingIndex = _pingIconIndex;
        }
    }  
}
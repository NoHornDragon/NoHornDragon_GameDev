using System.Collections.Generic;
using UnityEngine;


namespace NHD.UI.EmojiUI
{
    public class EmojiSpawner : MonoBehaviour
    {
        [SerializeField] private List<EmojiInGame> _emojiPrefab;
        private List<Queue<EmojiInGame>> _emojiPool = new List<Queue<EmojiInGame>>();
        
        [SerializeField] private Vector2 _emojiOffsetInGame;
        private Transform _prevTarget;
        private EmojiInGame _prevEmoji;


        private void Start()
        {
            for(int i = 0; i < _emojiPrefab.Count; i++)
            {
                _emojiPool.Add(new Queue<EmojiInGame>());
            }
        }

        public void SpawnEmoji(int index, Transform target)
        {
            var emoji = GetEmojiFromPool(index);

            emoji.transform.SetParent(target);
            emoji.gameObject.SetActive(true);
            emoji.transform.localPosition = _emojiOffsetInGame;
        }

        private EmojiInGame GetEmojiFromPool(int index)
        {
            if(_emojiPool[index].Count <= 0)
                SupplyEmojiPool(index);

            return _emojiPool[index].Dequeue();
        }

        private void ReturnEmojiToPool(EmojiInGame emoji)
        {
            _emojiPool[emoji._emojiIndex].Enqueue(emoji);
            emoji.transform.SetParent(this.gameObject.transform);
            emoji.gameObject.SetActive(false);
        }

        private void SupplyEmojiPool(int index)
        {
            var emoji = Instantiate(_emojiPrefab[index]);
            emoji._returnToPoolCallbackEvent += ReturnEmojiToPool;
            _emojiPool[index].Enqueue(emoji);
        }
    }
}


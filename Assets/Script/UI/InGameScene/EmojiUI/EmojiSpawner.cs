using System.Collections.Generic;
using UnityEngine;
using NHD.GamePlay.ObjectPool;

namespace NHD.UI.EmojiUI
{
    public class EmojiSpawner : MonoBehaviour, IObjectPool
    {
        [SerializeField] private List<PoolableObjectBase> _emojiPrefab;
        private List<Queue<PoolableObjectBase>> _emojiPool = new List<Queue<PoolableObjectBase>>();
        [SerializeField] private Vector2 _emojiOffsetInGame;
        private Transform _prevTarget;
        private EmojiInGame _prevEmoji;
        private int _curIndex;


        private void Start()
        {
            for(int i = 0; i < _emojiPrefab.Count; i++)
            {
                _emojiPool.Add(new Queue<PoolableObjectBase>());
            }
        }

        public void SpawnEmoji(int index, Transform target)
        {
            if(target == _prevTarget)
            {
                _prevEmoji.StopEmoji();
            }

            _curIndex = index;
            var emoji = GetObjectFromPool();

            emoji.transform.SetParent(target);
            emoji.gameObject.SetActive(true);
            emoji.transform.localPosition = _emojiOffsetInGame;

            _prevTarget = target;
            _prevEmoji = emoji.GetComponent<EmojiInGame>();
        }

        public PoolableObjectBase GetObjectFromPool()
        {
            if(_emojiPool[_curIndex].Count <= 0)
                SupplyObjectPool();

            return _emojiPool[_curIndex].Dequeue();
        }

        public void ReturnObjectToPool(PoolableObjectBase emoji)
        {
            var emojiIndex = emoji.GetComponent<EmojiInGame>()._emojiIndex;
            _emojiPool[emojiIndex].Enqueue(emoji);
            emoji.transform.SetParent(this.gameObject.transform);
            emoji.gameObject.SetActive(false);
        }

        public void SupplyObjectPool()
        {
            var emoji = Instantiate(_emojiPrefab[_curIndex]);
            emoji.ReturnToPoolCallbackEvent += ReturnObjectToPool;
            
            ReturnObjectToPool(emoji);
        }
    }
}


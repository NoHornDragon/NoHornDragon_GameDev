using UnityEngine;
using NHD.GamePlay.ObjectPool;
using DG.Tweening;

namespace NHD.UI.EmojiUI
{
    public class EmojiInGame : IPoolableObject
    {
        private Sequence _popupSequence;
        [SerializeField] private int _emojiIndexInBackGround;
        public int _emojiIndex { get{ return _emojiIndexInBackGround; } }

        void Start()
        {
            transform.localScale = Vector3.zero;
            Color startColor = GetComponent<SpriteRenderer>().color;
            startColor.a = 0;

            // make Tweening
            _popupSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .OnStart(() => {
                transform.localScale = Vector3.zero;
                GetComponent<SpriteRenderer>().color = startColor;
            })
            .Append(transform.DOScale(1, 1).SetEase(Ease.OutBounce))
            .Join(GetComponent<SpriteRenderer>().DOFade(1, 1))
            .SetDelay(0.5f)
            .AppendInterval(2f)
            .OnComplete(()=>{
                InvokeReturnCall();
            });
        }


        private void OnEnable()
        {
            _popupSequence.Restart();
        }

        private void OnDisable()
        {
            _popupSequence.Pause();
        }

        public void StopEmoji()
        {
            InvokeReturnCall();
        }
    }
}
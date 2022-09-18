using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

namespace NHD.UI.EmojiUI
{
    public class EmojiInGame : MonoBehaviour
    {
        private Sequence _popupSequence;
        public event Action _returnToPoolCallbackEvent;

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
                // Callback이 더 나은 방식으로 될 수 있는가?
                _returnToPoolCallbackEvent?.Invoke();
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
    }
}
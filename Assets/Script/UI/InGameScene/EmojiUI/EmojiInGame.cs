using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

namespace NHD.UI.EmojiUI
{
    public class EmojiInGame : MonoBehaviour
    {
        private Sequence _popupSequence;
        private Color _startColor;

        public event Action _returnCallbackEvent;

        void Start()
        {
            transform.localScale = Vector3.zero;
            _startColor = GetComponent<SpriteRenderer>().color;
            _startColor.a = 0;

            _popupSequence = DOTween.Sequence()
            .SetAutoKill(false)
            .OnStart(() => {
                transform.localScale = Vector3.zero;
                GetComponent<SpriteRenderer>().color = _startColor;
            })
            .Append(transform.DOScale(1, 1).SetEase(Ease.OutBounce))
            .Join(GetComponent<SpriteRenderer>().DOFade(1, 1))
            .SetDelay(0.5f)
            .AppendInterval(2f)
            .OnComplete(()=>{
                // TODO : Callback이 더 나은 방식으로 될 수 있는가?
                _returnCallbackEvent?.Invoke();
            });
        }


        private void OnEnable()
        {
            _popupSequence.Restart();
        }
    }
}
using DG.Tweening;
using UnityEngine;

namespace NHD.UI.UITweening
{
    public class FadeUITweening : MonoBehaviour
    {
        Sequence sequence;
        private CanvasGroup UIGroup;
        [SerializeField] private float UIIntervalTime = 2;
        void Start()
        {
            UIGroup = GetComponent<CanvasGroup>();

            sequence = DOTween.Sequence()
            .OnStart(() => { UIGroup.alpha = 0f; })
            .SetAutoKill(false)
            .Append(UIGroup.DOFade(1, 2))
            .AppendInterval(UIIntervalTime)
            .Append(UIGroup.DOFade(0, 2))
            .OnComplete(() => { this.gameObject.SetActive(false); });
        }

        private void OnEnable()
        {
            sequence.Restart();
        }
    }
}
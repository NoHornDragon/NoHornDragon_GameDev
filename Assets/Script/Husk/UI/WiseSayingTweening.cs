using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class WiseSayingTweening : MonoBehaviour
{
    Sequence wiseSayingSequence;
    private CanvasGroup UIGroup;
    [SerializeField] private float UIIntervalTime;
    void Start()
    {
        UIGroup = GetComponent<CanvasGroup>();

        wiseSayingSequence = DOTween.Sequence()
        .OnStart(() => { UIGroup.alpha = 0f; })
        .SetAutoKill(false)
        .Append(UIGroup.DOFade(1, 2))
        .AppendInterval(UIIntervalTime)
        .Append(UIGroup.DOFade(0, 2))
        .OnComplete(() => { this.gameObject.SetActive(false); });
    }

    private void OnEnable()
    {
        wiseSayingSequence.Restart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PaperSheet : MonoBehaviour
{
    private event Action paperGetEvent;

    [Tooltip("종이조각의 번호이며 0번부터 시작합니다")]
    [SerializeField] private int paperIndex;
    [Tooltip("좌우 회전 여부")]
    [SerializeField] private bool isLRMove;
    [Tooltip("움직이는 정도")]
    [SerializeField] private float moveAmount;

    private Sequence paperSequence;
    private Vector3 originPos;
    [SerializeField] private Ease ease;
    private bool playerGetThis;

    void Start()
    {
        paperGetEvent += FindObjectOfType<PaperGetUI>().PaperGet;

        // if(HistoryDataManager.instance.GetPaperTrue(paperIndex))
        // {
        //     Destroy(this.gameObject);
        // }

        originPos = this.transform.position;


        // make paper sequence
        paperSequence = DOTween.Sequence();
        if (isLRMove)
            paperSequence.Append(transform.DOMoveX(originPos.x + moveAmount, 2, false).SetEase(ease));
        else
            paperSequence.Append(transform.DOMoveY(originPos.y + moveAmount, 2, false).SetEase(ease));
        
        paperSequence.SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !playerGetThis)
        {
            Debug.Log("플레이어가 이 종이조각을 먹었습니다");
            PlayerGetPaper();
        }
    }

    private void PlayerGetPaper()
    {
        playerGetThis = true;
        paperGetEvent?.Invoke();
        
        // TODO : 플레이어 이후 수정
        // HistoryDataManager.instance.SetPaperTrue(paperIndex);

        // 사라지는 애니메이션
        paperSequence.Kill();


        Sequence endSequence = DOTween.Sequence();
        endSequence.Append(transform.DORotate(new Vector3(0, 0, -360), 0.3f, RotateMode.FastBeyond360)
        .SetLoops(5, LoopType.Incremental)
        .SetEase(Ease.Linear))
        .Join(transform.DOScale(0, 2).SetEase(Ease.OutBounce))
        .OnComplete(() => {
            endSequence.Kill();
        })
        .OnKill(() =>{
            Destroy(this.gameObject);
        });

    }
}

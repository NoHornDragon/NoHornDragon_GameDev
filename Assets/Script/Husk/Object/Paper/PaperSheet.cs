using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaperSheet : MonoBehaviour
{
    [Tooltip("종이조각의 번호이며 0번부터 시작합니다")]
    [SerializeField] private int paperIndex;
    private Sequence paperSequence;
    private Vector3 originPos;
    [SerializeField] private Ease ease;

    void Start()
    {
        if(SaveData.instance.userData.paperList[paperIndex])
        {
            Destroy(this.gameObject);
        }

        originPos = this.transform.position;

        paperSequence = DOTween.Sequence()
        .Append(transform.DOMoveY(originPos.y + 5f, 2, false).SetEase(ease))
        .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 이 종이조각을 먹었습니다");
        }
    }

    private void PlayerGetPaper()
    {
        SaveData.instance.userData.paperList[paperIndex] = true;

        // 사라지는 애니메이션
    }
}

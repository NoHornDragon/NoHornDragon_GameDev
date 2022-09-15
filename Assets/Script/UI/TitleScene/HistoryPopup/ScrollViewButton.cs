using System.Collections;
using UnityEngine;

namespace NHD.UI.TitleScene.HistoryPopup
{
    public class ScrollViewButton : MonoBehaviour
    {
        [SerializeField]
        private int MAX_PAGE; // 최대 페이지
        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private float nodeWidth; // 한 노드의 너비(간격 포함)
        [SerializeField]
        private int moveVal; // 한 번에 몇칸을 움직일 건지
        private int currentPage = 1; // 현재 페이지가 어디인지

        private float pos;
        private float movePos;

        // Start is called before the first frame update
        void Start()
        {
            pos = contentPanel.localPosition.x;
            movePos = nodeWidth * moveVal;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Right()
        {
            if (currentPage < MAX_PAGE)
            {
                movePos = pos - nodeWidth * moveVal;
                pos = movePos;
                StartCoroutine(ScrollMoving());
                currentPage++;
            }


        }

        public void Left()
        {
            if (currentPage > 1)
            {
                movePos = pos + nodeWidth * moveVal;
                pos = movePos;
                StartCoroutine(ScrollMoving());
                currentPage--;
            }
        }

        IEnumerator ScrollMoving()
        {
            float currentPositionX = contentPanel.localPosition.x;
            for (int i = 0; i < 100; i++)
            {
                contentPanel.localPosition = new Vector2(Mathf.Lerp(contentPanel.localPosition.x, movePos, 0.1f), contentPanel.localPosition.y);
                yield return null;
            }
            contentPanel.localPosition = new Vector2(movePos, contentPanel.localPosition.y);
        }
    }
}
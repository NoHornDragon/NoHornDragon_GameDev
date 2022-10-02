using NHD.StaticData.History;
using NHD.UI.Common;
using NHD.UI.titleScene.historyPopup.descriptionPopup;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.titleScene.historyPopup
{
    public class HistoryPopup : MonoBehaviour, IPopup
    {
        private const int POPUP_LAYER = 1;
        private const int MAX_PAPER_SCROLLVIEW_PAGE = 2;
        private const int NODE_WIDTH = 280;
        private const int MOVE_VAL = 5;
        private const int ENTIRE_PAPER_COUNT = 20;

        [SerializeField] private Button[] _paperButtons;
        [SerializeField] private Text _paperCount;
        [SerializeField] private Text _playTime;
        [SerializeField] private Text _stunCount;
        [SerializeField] private Text _restartCount;
        [SerializeField] RectTransform _paperContents;
        [SerializeField] GameObject _desriptionPopup;
        private Vector2 _destPos = new Vector2();
        private int _currentPage;
        private float _pos;
        private float _movePos;
        private bool _isContentScrolling;

        public void Setup()
        {
            this.gameObject.SetActive(true);
            SetDefaultValues();
            SetTexts();
            CheckPaperGet();
        }

        private void SetDefaultValues()
        {
            _currentPage = 1;
            _isContentScrolling = false;
            _destPos.x = 0;
            _destPos.y = _paperContents.localPosition.y;
            _paperContents.localPosition = _destPos;
            _pos = 0;
        }

        private void SetTexts()
        {
            _playTime.text = StaticHistoryData._totalPlayTime.ToString();
            _stunCount.text = StaticHistoryData._stunCount.ToString();
            _restartCount.text = StaticHistoryData._restartCount.ToString();
        }

        private void CheckPaperGet()
        {
            int totalGetPaper = 0;
            for(int i = 0; i < ENTIRE_PAPER_COUNT; ++i)
            {
                if (StaticHistoryData._isGetPapers[i])
                {
                    _paperButtons[i].interactable = true;
                    var paperImage = _paperButtons[i].transform.GetChild(0).GetComponent<Image>();
                    paperImage.sprite = Resources.Load<Sprite>(StaticHistoryData._nodes[i]._imagePath);
                    ++totalGetPaper;
                }
            }
            _paperCount.text = $"{totalGetPaper} / {ENTIRE_PAPER_COUNT}";
        }

        private void Update()
        {
            CheckKeyInput();
        }

        public void CheckKeyInput()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && PopupContainer._popupCount == POPUP_LAYER)
            {
                ClosePopup();
            }
        }

        public void ClosePopup()
        {
            PopupContainer.PopPopup();
            this.gameObject.SetActive(false);
        }

        public void ContentMoveToLeft()
        {
            if(_currentPage > 1 && !_isContentScrolling)
            {
                _isContentScrolling = true;
                _movePos = _pos + NODE_WIDTH * MOVE_VAL;
                _pos = _movePos;
                StartCoroutine(ScrollingMoveCoroutine());
                --_currentPage;
            }
        }

        public void ContentMoveToRight()
        {
            if(_currentPage < MAX_PAPER_SCROLLVIEW_PAGE && !_isContentScrolling)
            {
                _isContentScrolling = true;
                _movePos = _pos - NODE_WIDTH * MOVE_VAL;
                _pos = _movePos;
                StartCoroutine(ScrollingMoveCoroutine());
                ++_currentPage;
            }
        }

        IEnumerator ScrollingMoveCoroutine()
        {
            float currentPositionX = _paperContents.localPosition.x;

            for(int i = 0; i < 100; ++i)
            {
                _destPos.x = Mathf.Lerp(_paperContents.localPosition.x, _movePos, 0.1f);
                _destPos.y = _paperContents.localPosition.y;
                _paperContents.localPosition = _destPos;
                yield return null;
            }
            _destPos.x = _movePos;
            _destPos.y = _paperContents.localPosition.y;
            _paperContents.localPosition = _destPos;
            _isContentScrolling = false;
        }

        public void OpenDescriptionPopup(int paperIndex)
        {
            DescriptionPopup._paperIndex = paperIndex;
            PopupContainer.PushPopup(_desriptionPopup.GetComponent<IPopup>());
        }
    }
}
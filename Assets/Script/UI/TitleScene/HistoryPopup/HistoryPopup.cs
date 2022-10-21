
using DG.Tweening;
using NHD.StaticData.History;
using NHD.UI.Common;
using NHD.UI.titleScene.historyPopup.descriptionPopup;
using NHD.Utils.SoundUtil;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.titleScene.historyPopup
{
	public class HistoryPopup : MonoBehaviour, IPopup
    {
        private const int MAX_PAPER_SCROLLVIEW_PAGE = 2;
        private const int NODE_WIDTH = 280;
        private const int MOVE_VAL = 5;
        private const int ENTIRE_PAPER_COUNT = 20;
        private const float SCROLL_DURATION = 0.3f;

        [SerializeField] private Button[] _paperButtons;
        [SerializeField] private Text _paperCount;
        [SerializeField] private Text _playTime;
        [SerializeField] private Text _stunCount;
        [SerializeField] private Text _restartCount;
        [SerializeField] RectTransform _paperContents;
        [SerializeField] GameObject _desriptionPopup;
        [SerializeField] private AudioClip _closedSound;
        [SerializeField] private AudioClip _descriptionOpenSound;
        private int _currentPage;
        private float _pos;
        private float _movePos;

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
            _paperContents.localPosition = Vector2.zero;
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

        public void ClosePopup()
        {
            SoundManager._instance.PlayEFXAmbient(_closedSound);
            this.gameObject.SetActive(false);
        }

        public void ContentMoveToLeft()
        {
            if(_currentPage > 1)
            {
                SetDirection(false);
                _paperContents.DOAnchorPosX(_movePos, SCROLL_DURATION);
                --_currentPage;
            }
        }

        public void ContentMoveToRight()
        {
            if(_currentPage < MAX_PAPER_SCROLLVIEW_PAGE)
            {
                SetDirection(true);
                _paperContents.DOAnchorPosX(_movePos, SCROLL_DURATION);
                ++_currentPage;
            }
        }

        private void SetDirection(bool isRight)
		{
            _movePos = (isRight) ? _pos - NODE_WIDTH * MOVE_VAL : _pos + NODE_WIDTH * MOVE_VAL;
            _pos = _movePos;
        }

        public void OpenDescriptionPopup(int paperIndex)
        {
            SoundManager._instance.PlayEFXAmbient(_descriptionOpenSound);
            DescriptionPopup._paperIndex = paperIndex;
            PopupContainer.PushPopup(_desriptionPopup.GetComponent<IPopup>());
        }
    }
}
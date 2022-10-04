using DG.Tweening;
using NHD.StaticData.History;
using NHD.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.titleScene.historyPopup.descriptionPopup
{
	public class DescriptionPopup : MonoBehaviour, IPopup
    {
        private const float CLOSED_DESCRIPTION_POS_Y = -850;
        private const float OPEND_DESCRIPTION_POS_Y = 0;
        private const float UPDOWN_DURATION = 0.5f;

        [SerializeField] private GameObject _popupBackGround;
        [SerializeField] private Text _paperTitle;
        [SerializeField] private Text _paperDescription;
        [SerializeField] private Image _paperImage;
        private Vector2 _destPos = new Vector2();
        private RectTransform _rectTransform;

        public static int _paperIndex;

        public void Setup()
        {
            SetDefaultValues();
            SetPaperData();
            _popupBackGround.SetActive(true);
            _rectTransform.DOAnchorPosY(OPEND_DESCRIPTION_POS_Y, UPDOWN_DURATION).SetEase(Ease.OutCirc);
        }

        private void SetDefaultValues()
        {
            _destPos.x = GetComponent<RectTransform>().localPosition.x;
            _destPos.y = CLOSED_DESCRIPTION_POS_Y;
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localPosition = _destPos;
        }

        private void SetPaperData()
        {
            _paperTitle.text = StaticHistoryData._nodes[_paperIndex]._title;
            _paperDescription.text = StaticHistoryData._nodes[_paperIndex]._description;
            _paperImage.sprite = Resources.Load<Sprite>(StaticHistoryData._nodes[_paperIndex]._imagePath);
        }

        private void TurnOffBackGround()
		{
            _popupBackGround.SetActive(false);
		}

        public void ClosePopup()
        {
            _rectTransform.DOAnchorPosY(CLOSED_DESCRIPTION_POS_Y, UPDOWN_DURATION).SetEase(Ease.InCirc).OnComplete(TurnOffBackGround);
        }
    }
}
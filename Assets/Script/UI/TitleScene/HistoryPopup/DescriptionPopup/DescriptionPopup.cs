using NHD.StaticData.History;
using NHD.UI.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.titleScene.historyPopup.descriptionPopup
{
    public class DescriptionPopup : MonoBehaviour, IPopup
    {
        private const float CLOSED_DESCRIPTION_POS_Y = -850;
        private const float OPEND_DESCRIPTION_POS_Y = 0;

        [SerializeField] private GameObject _popupBackGround;
        [SerializeField] private Text _paperTitle;
        [SerializeField] private Text _paperDescription;
        [SerializeField] private Image _paperImage;
        private Vector2 _destPos = new Vector2();
        private RectTransform _rectTransform;
        private bool _isDescriptionPopupOpening;

        public static int _paperIndex;

        public void Setup()
        {
            SetDefaultValues();
            SetPaperData();
            StartCoroutine(PopupMovingCoroutine(true));
        }

        private void SetDefaultValues()
        {
            _isDescriptionPopupOpening = false;
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

        IEnumerator PopupMovingCoroutine(bool isOpen)
        {
            _isDescriptionPopupOpening = true;

            float destPosY = SetDestPos(isOpen);

            if (isOpen)
            {
                _popupBackGround.SetActive(true);
            }

            for (int i = 0; i < 50; ++i)
            {
                _destPos.x = _rectTransform.localPosition.x;
                _destPos.y = Mathf.Lerp(_rectTransform.localPosition.y, destPosY, 0.2f);
                _rectTransform.localPosition = _destPos;
                yield return null;
            }
            _destPos.x = _rectTransform.localPosition.x;
            _destPos.y = destPosY;
            _rectTransform.localPosition = _destPos;
            _isDescriptionPopupOpening = false;

            if (!isOpen)
            {
                _popupBackGround.SetActive(false);
            }
        }

        private float SetDestPos(bool isOpen)
        {
            float destPosY;

            if (isOpen)
            {
                destPosY = OPEND_DESCRIPTION_POS_Y;
            }
            else
            {
                destPosY = CLOSED_DESCRIPTION_POS_Y;
            }

            return destPosY;
        }

        public void ClosePopup()
        {
            StartCoroutine(PopupMovingCoroutine(false));
        }
    }
}
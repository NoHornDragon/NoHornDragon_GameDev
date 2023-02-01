using NHD.StaticData.History;
using NHD.UI.Common;
using NHD.Utils.SoundUtil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.titleScene.historyPopup
{
    public class HistoryPopup : MonoBehaviour, IPopup
    {
        private const int ENTIRE_PAPER_COUNT = 20;

        [SerializeField] private Button[] _paperButtons;
        [SerializeField] private TextMeshProUGUI _paperCount;
        [SerializeField] private TextMeshProUGUI _playTime;
        [SerializeField] private TextMeshProUGUI _stunCount;
        [SerializeField] private TextMeshProUGUI _restartCount;
        [SerializeField] private AudioClip _closedSound;
        [SerializeField] private AudioClip _descriptionOpenSound;
        [SerializeField] private TextMeshProUGUI _paperTitle;
        [SerializeField] private TextMeshProUGUI _paperDescription;
        [SerializeField] private Image _paperImage;

        public void Setup()
        {
            this.gameObject.SetActive(true);
            SetTexts();
            CheckPaperGet();
            ClearDescription();
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
            ClearDescription();
            this.gameObject.SetActive(false);
        }

        public void ShowDescription(int paperIndex)
        {
            SoundManager._instance.PlayEFXAmbient(_descriptionOpenSound);
            _paperTitle.text = StaticHistoryData._nodes[paperIndex]._title;
            _paperDescription.text = StaticHistoryData._nodes[paperIndex]._description;
            _paperImage.sprite = Resources.Load<Sprite>(StaticHistoryData._nodes[paperIndex]._imagePath);
        }

        private void ClearDescription()
        {
            _paperTitle.text = "";
            _paperDescription.text = "";
            _paperImage.sprite = null;
        }
    }
}
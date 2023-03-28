using NHD.DataController.Savers;
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
        [SerializeField] private Button[] _paperButtons;
        [SerializeField] private TextMeshProUGUI _paperCount;
        [SerializeField] private TextMeshProUGUI _playTime;
        [SerializeField] private TextMeshProUGUI _stunCount;
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
        }

        private void CheckPaperGet()
        {
            int totalGetPaper = 0;
            for(int i = 0; i < _paperButtons.Length; ++i)
            {
                if (StaticHistoryData._isGetPapers[i])
                {
                    _paperButtons[i].interactable = true;
                    //var paperImage = _paperButtons[i].transform.GetChild(0).GetComponent<Image>();
                    var paperImage = _paperButtons[i].transform.GetChild(1).GetChild(1).GetComponent<Image>();
                    paperImage.sprite = Resources.Load<Sprite>(StaticHistoryData._nodes[i]._imagePath);
                    paperImage.color = new Color(1, 1, 1, 1);
                    ++totalGetPaper;

                    if(StaticHistoryData._isNewPaper[i])
                    {
                        _paperButtons[i].transform.GetChild(2).gameObject.SetActive(true);
                    }
                }
            }
            _paperCount.text = $"{totalGetPaper} / {StaticHistoryData._isGetPapers.Length}";
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
            _paperImage.color = new Color(1, 1, 1, 1);
            RemoveNewMark(paperIndex);
            SelectPaper(paperIndex);
        }

        private void RemoveNewMark(int paperIndex)
        {
            StaticHistoryData._isNewPaper[paperIndex] = false;
            PlayHistoryDataSaver.SaveData();
            _paperButtons[paperIndex].transform.GetChild(2).gameObject.SetActive(false);
        }

        private void SelectPaper(int paperIndex)
        {
            for (int i = 0; i < _paperButtons.Length; ++i)
            {
                _paperButtons[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            _paperButtons[paperIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        private void ClearDescription()
        {
            _paperTitle.text = "";
            _paperDescription.text = "";
            _paperImage.color = new Color(1, 1, 1, 0);
            _paperImage.sprite = null;
        }
    }
}
using NHD.Utils.SettingUtil;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.TitleScene.HistoryPopup
{
    public class HistoryManager : MonoBehaviour
    {
        // 총 종이조각의 수
        public const int TOTAL_PAPER = 20;

        public static bool isDescriptionMoving = false;
        public static bool isDescriptionOpen = false;

        [SerializeField]
        private HistoryNode[] nodes;

        [Header("UI Element")]
        [SerializeField]
        private Text paperInfoText;
        [SerializeField]
        private Text playTimeText;
        [SerializeField]
        private Text stunText;
        [SerializeField]
        private Text restartText;

        [Header("DescriptionPanel 관련")]
        [SerializeField]
        private GameObject DescriptionPanel;
        [SerializeField]
        private RectTransform descriptionBox;
        [SerializeField]
        private Text nodeName;
        [SerializeField]
        private Image nodeImage;
        [SerializeField]
        private Text nodeDescription;

        // 현재 보유한 종이조각의 수
        private int paperCount;

        private HistoryData historyData;
        public void Start()
        {
            paperCount = 0;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isDescriptionOpen && !isDescriptionMoving)
                DescriptionPanelClose();
        }
        public void ActiveNodes()
        {
            historyData = HistoryDataManager.instance.GetHistoryData();
            for (int i = 0; i < nodes.Length; i++)
            {
                if (!historyData.activeNodes[i])
                    nodes[i].GetComponent<Button>().interactable = false;
                else
                {
                    nodes[i].childImage.sprite = nodes[i].image;
                    paperCount++;
                }
            }

            paperInfoText.text = paperCount + "/" + TOTAL_PAPER;
            playTimeText.text = historyData.playTime.ToString();
            stunText.text = historyData.stunCount.ToString();
            restartText.text = historyData.restartCount.ToString();

            paperCount = 0;
        }
        public void DescriptionPanelOpen(int _val)
        {
            if (!isDescriptionOpen && !isDescriptionMoving)
            {
                DescriptionPanel.SetActive(true);
                nodeName.text = nodes[_val].title[SettingsManager.instance.languageDropdown.value];
                nodeImage.sprite = nodes[_val].image;
                nodeDescription.text = nodes[_val].description[SettingsManager.instance.languageDropdown.value];
                StartCoroutine(DescriptionPanelCoroutine(true)); // -> isDescriptionOpen = true
            }
        }

        public void DescriptionPanelClose()
        {
            if (isDescriptionOpen && !isDescriptionMoving)
            {
                StartCoroutine(DescriptionPanelCoroutine(false)); // -> isDescriptionOpen = false
                DescriptionPanel.SetActive(false);
            }
        }
        IEnumerator DescriptionPanelCoroutine(bool _panelSet)
        {
            isDescriptionMoving = true;
            float destPosY;
            if (!_panelSet)
                destPosY = -850;
            else
                destPosY = 0;

            for (int i = 0; i < 50; i++)
            {
                descriptionBox.localPosition = new Vector2(descriptionBox.localPosition.x, Mathf.Lerp(descriptionBox.localPosition.y, destPosY, 0.2f));
                yield return null;
            }
            isDescriptionOpen = !isDescriptionOpen;
            isDescriptionMoving = false;
        }
    }
}
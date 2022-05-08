using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HistoryManager : MonoBehaviour
{
    public static bool isDescriptionMoving = false;
    public static bool isDescriptionOpen = false;

    [SerializeField]
    private HistoryNode[] nodes;
    [SerializeField]
    private bool[] getNodes;

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

    public void Start()
    {
        ActiveNodes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isDescriptionOpen && !isDescriptionMoving)
            DescriptionPanelClose();
    }
    public void ActiveNodes()
    {
        for(int i = 0; i < nodes.Length; i++)
        {
            if (!getNodes[i])
                nodes[i].GetComponent<Button>().interactable = false;
            else
                nodes[i].childImage.sprite = nodes[i].image;
        }
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

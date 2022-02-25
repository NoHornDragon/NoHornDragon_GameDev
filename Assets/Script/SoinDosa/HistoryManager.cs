using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HistoryManager : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Escape) && isDescriptionOpen)
            DescriptionPanelClose();
    }
    public void ActiveNodes()
    {
        for(int i = 0; i < nodes.Length; i++)
        {
            if (!getNodes[i])
                nodes[i].GetComponent<Button>().interactable = false;
        }
    }
    public void DescriptionPanelOpen(int _val)
    {
        isDescriptionOpen = true;
        DescriptionPanel.SetActive(true);
        nodeName.text = nodes[_val].title;
        nodeImage.sprite = nodes[_val].image;
        nodeDescription.text = nodes[_val].description;
        StartCoroutine(DescriptionPanelCoroutine(0f));
    }

    public void DescriptionPanelClose()
    {
        isDescriptionOpen = false;
        StartCoroutine(DescriptionPanelCoroutine(-1000f));
        DescriptionPanel.SetActive(false);
    }
    IEnumerator DescriptionPanelCoroutine(float _destPosY)
    {
        for (int i = 0; i < 100; i++)
        {
            descriptionBox.localPosition = new Vector2(descriptionBox.localPosition.x, Mathf.Lerp(descriptionBox.localPosition.y, _destPosY, 0.1f));
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HistoryManager : MonoBehaviour
{
    [SerializeField]
    private HistoryNode[] nodes;
    [SerializeField]
    private bool[] getNodes;

    [Header("DescriptionPanel 관련")]
    [SerializeField]
    private GameObject DescriptionPanel;
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
        DescriptionPanel.SetActive(true);
        nodeName.text = nodes[_val].title;
        nodeImage.sprite = nodes[_val].image;
        nodeDescription.text = nodes[_val].description;
    }

    public void DescriptionPanelClose()
    {
        DescriptionPanel.SetActive(false);
    }
}

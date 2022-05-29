using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
public class WiseSayingTeller : MonoBehaviour
{   
    
    [SerializeField] [TextArea]
    List<string> wiseSayings;
    [SerializeField] private GameObject wiseSayingUI;
    [SerializeField] private TextMeshProUGUI wiseSayingText;
    void Start()
    {
        wiseSayingUI.SetActive(false);
    }

    [ContextMenu("명언 UI ON")]
    private void ShowWiseSayingOnScreen()
    {

        // Have a appear tweening wiseSayingUI's OnEnable()
        // And will disappear automatically
        wiseSayingText.text = wiseSayings[Random.Range(0, wiseSayings.Count)];
        wiseSayingUI.SetActive(true);

    }
}
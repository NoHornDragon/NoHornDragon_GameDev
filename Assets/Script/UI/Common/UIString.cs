using NHD.StaticData.UIString;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIString : MonoBehaviour
{
    [SerializeField] private string _stringKey;
    private Text _uiText;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiText = GetComponent<Text>();
        _uiText.text = StaticUIString._staticUIString[_stringKey];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

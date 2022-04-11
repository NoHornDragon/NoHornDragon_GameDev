using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIText : MonoBehaviour
{
    [Header("text[0] = kor, text[1] = eng")]
    public string[] texts;

    public Text uiText;
    private void Awake()
    {
        uiText = this.GetComponent<Text>();
    }

    private void Start()
    {
        LangChange(SettingsManager.instance.languageDropdown.value);
        SettingsManager.instance.LanguageChangeEvent += LangChange;
    }

    private void OnDisable()
    {
        SettingsManager.instance.LanguageChangeEvent -= LangChange;
    }
    public void LangChange(int _val)
    {
        Debug.Log("Lang Change _val = " + _val);
        Debug.Log("Text 0 = " + texts[0] + ", Text 1 = " + texts[1]);
        uiText.text = texts[_val];
    }
}

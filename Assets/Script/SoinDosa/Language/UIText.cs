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


	private void OnDestroy()
    {
        SettingsManager.instance.LanguageChangeEvent -= LangChange;
    }
    public void LangChange(int _val)
    {
        uiText.text = texts[_val];
    }
}

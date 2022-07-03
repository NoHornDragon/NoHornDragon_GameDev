using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class SettingsValue
{
    public SettingsValue(int _resolutionVal, float _bgmVal, float _effectVal, int _languageVal, bool _isAutoSave)
    {
        this.resolutionVal = _resolutionVal;
        this.bgmVal = _bgmVal;
        this.effectVal = _effectVal;
        this.languageVal = _languageVal;
        this.isAutoSave = _isAutoSave;
    }
    public int resolutionVal;
    public float bgmVal;
    public float effectVal;
    public int languageVal;
    public bool isAutoSave;
}
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    public delegate void LanguageDelegate(int _val);
    public LanguageDelegate LanguageChangeEvent;

    [SerializeField]
    private Dropdown resolutionDropdown;
    private bool isAutoSaving;

    public Dropdown languageDropdown;
    public Slider bgmSlider;
    public Slider effectSlider;
    public Toggle autoSaveToggle;
    public GameObject autoSavePanel;
    public GameObject dataResetPanel;
    // Start is called before the first frame update

    private SoundManager sm;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);

        
    }

    private void Start()
    {
        Debug.Log("SettingsManager Start!");
        sm = FindObjectOfType<SoundManager>();
        LoadSettingsValue();
    }

    public void ChangeResolution()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                Debug.Log("Resolution Changed to FHD");
                break;
            case 1:
                Screen.SetResolution(1280, 720, true);
                Debug.Log("Resolution Changed to HD");
                break;
            default:
                break;
        }
    }
    public void ChangeLanguage()
    {
        if(LanguageChangeEvent != null)
        {
            switch (languageDropdown.value)
            {
                case 0:
                    LanguageChangeEvent(0);
                    break;
                case 1:
                    LanguageChangeEvent(1);
                    break;
                default:
                    break;
            }
        }
    }

    public void ChangeBGMVolume()
    {
        sm.audioSourceBGM.volume = bgmSlider.value;
    }

    public void ChangeEffectVolume()
    {
        foreach (var audioSourceEffect in sm.audioSourceEffects)
        {
            audioSourceEffect.volume = effectSlider.value;
        }
    }

    public void AutoSaveAsk()
    {
        if (autoSaveToggle.isOn && !isAutoSaving)
        {
            autoSavePanel.SetActive(true);
        }
        else if(!autoSaveToggle.isOn)
        {
            isAutoSaving = false;
        }
    }

    public void AutoSaveSet(bool _val)
    {
        autoSavePanel.SetActive(false);
        autoSaveToggle.isOn = _val;
    }

    public void DataResetAsk(bool _val)
    {
        dataResetPanel.SetActive(_val);
    }

    public void DataReset()
    {
        Debug.Log("Data reset!");
        dataResetPanel.SetActive(false);
    }

    public void SaveSettingsValue()
    {
        SettingsValue saveData = new SettingsValue(resolutionDropdown.value, bgmSlider.value, effectSlider.value, languageDropdown.value, autoSaveToggle.isOn);

        string jsonData = JsonUtility.ToJson(saveData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        FileStream fs = new FileStream(Application.persistentDataPath + "/SettingsValue.json", FileMode.Create);
        fs.Write(data, 0, data.Length);
        fs.Close();
    }

    public void LoadSettingsValue()
    {
        if (!File.Exists(Application.persistentDataPath + "/SettingsValue.json"))
        {
            Debug.Log("No Settings Value!");
            return;
        }
        FileStream fs = new FileStream(Application.persistentDataPath + "/SettingsValue.json", FileMode.Open);

        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        SettingsValue saveData = JsonUtility.FromJson<SettingsValue>(jsonData);

        resolutionDropdown.value = saveData.resolutionVal;
        bgmSlider.value = saveData.bgmVal;
        effectSlider.value = saveData.effectVal;
        languageDropdown.value = saveData.languageVal;
        if (saveData.isAutoSave)
            isAutoSaving = true;
        autoSaveToggle.isOn = saveData.isAutoSave;
        

        ChangeResolution();
        ChangeLanguage();
    }
}

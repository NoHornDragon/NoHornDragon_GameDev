using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class SettingsValue
{
    public SettingsValue(int _resolutionVal, float _bgmVal, float _effectVal)
    {
        this.resolutionVal = _resolutionVal;
        this.bgmVal = _bgmVal;
        this.effectVal = _effectVal;
    }
    public int resolutionVal;
    public float bgmVal;
    public float effectVal;
}
public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    [SerializeField]
    private Dropdown resolutionDropdown;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider effectSlider;
    // Start is called before the first frame update

    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        LoadSettingsValue();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void SaveSettingsValue()
    {
        SettingsValue saveData = new SettingsValue(resolutionDropdown.value, bgmSlider.value, effectSlider.value);

        string jsonData = JsonUtility.ToJson(saveData);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        FileStream fs = new FileStream(Application.dataPath + "SettingsValue.json", FileMode.Create);
        fs.Write(data, 0, data.Length);
        fs.Close();
    }

    public void LoadSettingsValue()
    {
        if (!File.Exists(Application.dataPath + "SettingsValue.json"))
        {
            Debug.Log("No Settings Value!");
            return;
        }
        FileStream fs = new FileStream(Application.dataPath + "SettingsValue.json", FileMode.Open);

        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        fs.Close();

        string jsonData = Encoding.UTF8.GetString(data);
        SettingsValue saveData = JsonUtility.FromJson<SettingsValue>(jsonData);

        resolutionDropdown.value = saveData.resolutionVal;
        bgmSlider.value = saveData.bgmVal;
        effectSlider.value = saveData.effectVal;
    }
}

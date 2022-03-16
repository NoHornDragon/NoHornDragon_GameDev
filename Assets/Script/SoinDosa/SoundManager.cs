using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSourceBGM;
    public AudioSource[] audioSourceEffects;

    public Sound[] bgmSounds;
    public Sound[] effectSounds;

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
        SetBGMVol();
        SetEffectVol();
    }

    public void SetBGMVol()
    {
        audioSourceBGM.volume = SettingsManager.instance.bgmSlider.value;
    }

    public void SetEffectVol()
    {
        foreach(var audioSourceEffect in audioSourceEffects)
        {
            audioSourceEffect.volume = SettingsManager.instance.effectSlider.value;
        }
    }

    public void PlayBGM(string _name)
    {
        foreach(var bgmSound in bgmSounds)
        {
            if(_name == bgmSound.name)
            {
                audioSourceBGM.clip = bgmSound.clip;
                audioSourceBGM.Play();
            }
        }
    }
}

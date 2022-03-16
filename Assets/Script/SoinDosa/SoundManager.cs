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

    [Header("재생가능한 오디오 소스")]
    public AudioSource audioSourceBGM;
    public AudioSource[] audioSourceEffects;

    [Header("게임에 사용되는 사운드")]
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

    public void PlayBGM(string _name, bool _isFade) 
    {
        if (_isFade) // Fade와 함께 bgm 실행
        {
            StartCoroutine(FadeInCoroutine(_name));
        }
        else // 즉시 bgm 실행
        {
            foreach (var bgmSound in bgmSounds) 
            {
                if (_name == bgmSound.name)
                {
                    audioSourceBGM.clip = bgmSound.clip;
                    audioSourceBGM.Play();
                    return;
                }
            }
            return;
        }
        return;
    }

    public void StopBGM(bool _isFade) // 즉시 bgm 중지
    {
        if (_isFade)
        {
            StartCoroutine(FadeOutCoroutine());
        }
        else
        {
            audioSourceBGM.Stop();
        }
    }

    IEnumerator FadeInCoroutine(string _name)
    {
        if (audioSourceBGM.isPlaying)
        {
            for (int i = 0; i < 10; i++)
            {
                audioSourceBGM.volume = Mathf.Lerp(audioSourceBGM.volume, 0.0f, 0.2f);
                yield return new WaitForSeconds(0.1f);
            }
            audioSourceBGM.Stop();
            audioSourceBGM.volume = SettingsManager.instance.effectSlider.value;
        }

        foreach (var bgmSound in bgmSounds)
        {
            if (_name == bgmSound.name)
            {
                audioSourceBGM.volume = 0.0f;
                audioSourceBGM.clip = bgmSound.clip;
                audioSourceBGM.Play();
                for (int i = 0; i < 20; i++)
                {
                    audioSourceBGM.volume = Mathf.Lerp(audioSourceBGM.volume, SettingsManager.instance.effectSlider.value, 0.1f);
                    yield return new WaitForSeconds(0.1f);
                }
                audioSourceBGM.volume = SettingsManager.instance.effectSlider.value;
            }
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        for(int i = 0; i < 10; i++)
        {
            audioSourceBGM.volume = Mathf.Lerp(audioSourceBGM.volume, 0.0f, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }
        audioSourceBGM.Stop();
        audioSourceBGM.volume = SettingsManager.instance.effectSlider.value;
    }

    public void PlaySE(string _name)
    {
        foreach(var effectSound in effectSounds)
        {
            if(_name == effectSound.name)
            {
                for(int i = 0; i < audioSourceEffects.Length; i++)
                {
                    if(!audioSourceEffects[i].isPlaying)
                    {
                        audioSourceEffects[i].clip = effectSound.clip;
                        audioSourceEffects[i].Play();
                        return;
                    }
                }
            }
        }
    }

    public void StopAllSE()
    {
        foreach(var audioSourceEffect in audioSourceEffects)
        {
            audioSourceEffect.Stop();
        }
    }
}

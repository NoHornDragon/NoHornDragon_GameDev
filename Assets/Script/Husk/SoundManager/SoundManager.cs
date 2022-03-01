using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource BackGroundSound;
    public AudioClip[] stagesSound;
    int curStageBGIndex = -1;
    public GameObject SFXPlayer;

    private void Awake() 
    {
        var obj = FindObjectsOfType<SoundManager>();
        if(obj.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void BackGroundSoundPlay(int soundIndex)
    {
        BackGroundSound.clip = stagesSound[soundIndex];
        BackGroundSound.loop = true;
        // BGsound.volume = 0.5f;
        BackGroundSound.Play();
    }

    public void SFXPlay(AudioClip clip)
    {   
        AudioSource audioSource = SFXPlayer.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(SFXPlayer.GetComponent<AudioSource>(), clip.length);
    }
}
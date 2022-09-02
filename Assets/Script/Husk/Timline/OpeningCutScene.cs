using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class OpeningCutScene : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;

    private bool skipped = false;

    public void SkipButtonPressed(float skipTime)
    {
        if(skipped) return;

        playableDirector.time = skipTime;
        skipped = true;
    }

    public void AfterTimeline()
    {
        SceneManager.LoadSceneAsync("Dev_husk");
    }
}

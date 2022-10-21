using NHD.UI.Common;
using NHD.Utils.SoundUtil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.titleScene.eixtAskingPopup
{
    public class ExitAskingPopup : MonoBehaviour, IPopup
    {
        [SerializeField] private AudioClip _closedSound;

        public void Setup() 
        {
            this.gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
            SoundManager._instance.PlayEFXAmbient(_closedSound);
            this.gameObject.SetActive(false);
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

    }
}
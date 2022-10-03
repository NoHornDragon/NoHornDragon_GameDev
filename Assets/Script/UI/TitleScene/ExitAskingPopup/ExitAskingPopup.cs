using NHD.UI.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.titleScene.eixtAskingPopup
{
    public class ExitAskingPopup : MonoBehaviour, IPopup
    {
        public void Setup() 
        {
            this.gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
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
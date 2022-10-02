using NHD.UI.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NHD.UI.titleScene.eixtAskingPopup
{
    public class ExitAskingPopup : MonoBehaviour, IPopup
    {
        private const int POPUP_LAYER = 1;

        public void Setup() 
        {
            this.gameObject.SetActive(true);
        }

        private void Update()
        {
            CheckKeyInput();
        }

        public void CheckKeyInput()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && PopupContainer._popupCount == POPUP_LAYER)
            {
                ClosePopup();
            }
        }

        public void ClosePopup()
        {
            PopupContainer.PopPopup();
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
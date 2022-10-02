using NHD.StaticData.Settings;
using NHD.UI.Common;
using UnityEngine;

namespace NHD.UI.titleScene.settingsPopup.autoSaveAskingPopup
{
    public class AutoSaveAskingPopup : MonoBehaviour, IPopup
    {
        private const int POPUP_LAYER = 2;

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
            OnInputEscKey();
        }

        private void OnInputEscKey()
        {
            if(Input.GetKeyUp(KeyCode.Escape) && PopupContainer._popupCount == POPUP_LAYER)
            {
                SetAutoSaveFalse();
            }
        }

        public void SetAutoSaveTrue()
        {
            StaticSettingsData._isAutoSave = true;
            ClosePopup();
        }

        public void SetAutoSaveFalse()
        {
            SettingsPopup parentPopup = transform.GetComponentInParent<SettingsPopup>();
            parentPopup._autoSave.isOn = false;
            ClosePopup();
        }

        public void ClosePopup()
        {
            PopupContainer.PopPopup();
            this.gameObject.SetActive(false);
        }
    }
}
using NHD.StaticData.Settings;
using NHD.UI.Common;
using UnityEngine;

namespace NHD.UI.titleScene.settingsPopup.autoSaveAskingPopup
{
    public class AutoSaveAskingPopup : MonoBehaviour, IPopup
    {
        private bool _isAgree;

        public void Setup()
        {
            this.gameObject.SetActive(true);
            _isAgree = false;
        }

        public void SetIsAgree(bool isAgree)
        {
            _isAgree = isAgree;
        }

        public void SetAutoSave()
        {
            if (_isAgree)
            {
                StaticSettingsData._isAutoSave = true;
            }
            else
            {
                SettingsPopup parentPopup = transform.GetComponentInParent<SettingsPopup>();
                parentPopup._autoSave.isOn = false;
            }
        }

        public void ClosePopup()
        {
            SetAutoSave();
            this.gameObject.SetActive(false);
        }
    }
}
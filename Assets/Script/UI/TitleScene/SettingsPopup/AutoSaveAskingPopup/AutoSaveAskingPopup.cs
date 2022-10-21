using NHD.StaticData.Settings;
using NHD.UI.Common;
using NHD.UI.inGameScene.pausePopup;
using NHD.Utils.SoundUtil;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NHD.UI.titleScene.settingsPopup.autoSaveAskingPopup
{
    public class AutoSaveAskingPopup : MonoBehaviour, IPopup
    {
        [SerializeField] private AudioClip _closedSound;
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
                if(SceneManager.GetActiveScene().name == "LobbyScene")
                {
                    var parentPopup = transform.GetComponentInParent<SettingsPopup>();
                    parentPopup._autoSave.isOn = false;
                }
                else
                {
                    var parentPopup = transform.GetComponentInParent<PausePopup>();
                    parentPopup._autoSave.isOn = false;
                }
            }
        }

        public void ClosePopup()
        {
            SoundManager._instance.PlayEFXAmbient(_closedSound);
            SetAutoSave();
            this.gameObject.SetActive(false);
        }
    }
}
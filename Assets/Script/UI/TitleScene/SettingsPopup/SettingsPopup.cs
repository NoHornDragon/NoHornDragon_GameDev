using NHD.DataController.Loaders;
using NHD.DataController.Savers;
using NHD.StaticData.Settings;
using NHD.UI.Common;
using NHD.Utils.SoundUtil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.titleScene.settingsPopup
{
    public class SettingsPopup : MonoBehaviour, IPopup
    {
        [SerializeField] private GameObject _autoSaveAskingPopup;
        [SerializeField] private GameObject _dataResetAskingPopup;
        [SerializeField] private TMP_Dropdown _resolution;
        [SerializeField] private TMP_Dropdown _language;
        [SerializeField] private Slider _bgmVolume;
        [SerializeField] private Slider _effectVolume;
        [SerializeField] private Toggle _difficulty;
        [SerializeField] private AudioClip _closedSound;
        [SerializeField] private AudioClip _askingSound;
        //public Toggle _autoSaveYES;
        //public Toggle _autoSaveNO;

        public void Setup()
        {
            this.gameObject.SetActive(true);

            _resolution.value = StaticSettingsData._resolutionIndex;
            SetResolution();
            _language.value = StaticSettingsData._languageIndex;
            _bgmVolume.value = StaticSettingsData._bgmVolume;
            _effectVolume.value = StaticSettingsData._effectVolume;
            _difficulty.isOn = StaticSettingsData._isHardMode;
            //_autoSaveYES.isOn = StaticSettingsData._isAutoSave;
        }

        private void SetResolution()
        {
            switch (StaticSettingsData._resolutionIndex)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, false);
                    break;
                case 1:
                    Screen.SetResolution(1280, 720, false);
                    break;
                default:
                    break;
            }
        }

        public void ClosePopup()
        {
            SoundManager._instance.PlayEFXAmbient(_closedSound);
            SaveSettingsData();
            this.gameObject.SetActive(false);
        }

        private void SaveSettingsData()
        {
            SettingsDataSaver.SaveData();
        }

        public void ChangeResolution()
        {
            StaticSettingsData._resolutionIndex = _resolution.value;
            SetResolution();
        }

        public void ChangeLanguage()
        {
            StaticSettingsData._languageIndex = _language.value;
            UIStringLoader.SetupData();
            PaperDataLoader.SetupData();
            NPCCommentsDataLoader.SetupData();
            WiseSayingLoader.SetupData();
            UIStringLoader.LanguageChangeAction();
        }

        public void ChangeBGMVolume()
        {
            StaticSettingsData._bgmVolume = _bgmVolume.value;
            SoundManager._instance.SetBGMVolume(_bgmVolume.value);
        }

        public void ChangeEffectVolume()
        {
            StaticSettingsData._effectVolume = _effectVolume.value;
            SoundManager._instance.SetEFXVolume(_effectVolume.value);
        }

        public void ChangeDifficulty()
        {
            StaticSettingsData._isHardMode = _difficulty.isOn;
        }

        //public void TurnOnAutoSave()
        //{
        //    if(_autoSaveYES.isOn == true && !StaticSettingsData._isAutoSave)
        //    {
        //        SoundManager._instance.PlayEFXAmbient(_askingSound);
        //        PopupContainer.PushPopup(_autoSaveAskingPopup.GetComponent<IPopup>());
        //    }

        //    StaticSettingsData._isAutoSave = _autoSaveYES.isOn;
        //}

        public void OpenResetAskingPopup()
        {
            SoundManager._instance.PlayEFXAmbient(_askingSound);
            PopupContainer.PushPopup(_dataResetAskingPopup.GetComponent<IPopup>());
        }
    }
}
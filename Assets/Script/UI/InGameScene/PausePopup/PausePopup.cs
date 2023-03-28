using NHD.DataController.Savers;
using NHD.StaticData.Settings;
using NHD.UI.Common;
using NHD.Utils.SceneUtil;
using NHD.Utils.SoundUtil;
using UnityEngine;
using UnityEngine.UI;

namespace NHD.UI.inGameScene.pausePopup
{
    public class PausePopup : MonoBehaviour, IPopup
    {
		private const string TITLE_SCENE_PATH = "LobbyScene";

		[SerializeField] private GameObject _autoSaveAskingPopup;
		[SerializeField] private Slider _bgmVolume;
		[SerializeField] private Slider _effectVolume;
		[SerializeField] private AudioClip _closedSound;
		[SerializeField] private AudioClip _popupOpenSound;
		// public Toggle _autoSaveYES;
		// public Toggle _autoSaveNO;

		private bool _isSceneLoading;

		public void Setup()
		{
			this.gameObject.SetActive(true);
			_isSceneLoading = transform.GetComponentInParent<InGameScene>()._isSceneLoading;
			_bgmVolume.value = StaticSettingsData._bgmVolume;
			_effectVolume.value = StaticSettingsData._effectVolume;
			//_autoSaveYES.isOn = StaticSettingsData._isAutoSave;
			Time.timeScale = 0.0f;
		}

		public void ClosePopup()
		{
			SoundManager._instance.PlayEFXAmbient(_closedSound);
			Time.timeScale = 1.0f;
			SaveSettingsData();
			this.gameObject.SetActive(false);
		}

		private void SaveSettingsData()
		{
			SettingsDataSaver.SaveData();
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

		//public void TurnOnAutoSave()
		//{
		//	if (_autoSaveYES == true && !StaticSettingsData._isAutoSave)
		//	{
		//		SoundManager._instance.PlayEFXAmbient(_popupOpenSound);
		//		PopupContainer.PushPopup(_autoSaveAskingPopup.GetComponent<IPopup>());
		//	}

		//	StaticSettingsData._isAutoSave = _autoSaveYES.isOn;
		//}

		public void QuitGame()
		{
			#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
			#else
			    Application.Quit();
			#endif
		}

		public void GoToTitleScene()
		{
			SoundManager._instance.PlayEFXAmbient(_closedSound);
			SaveSettingsData();
			Time.timeScale = 1.0f;
			_isSceneLoading = true;
			SceneChangerSingleton._instance.ChangeSceneWithFadeOut(TITLE_SCENE_PATH);
		}
	}
}

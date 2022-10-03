using NHD.DataController.Savers;
using NHD.StaticData.Settings;
using NHD.UI.Common;
using NHD.Utils.SceneUtil;
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
		public Toggle _autoSave;

		private bool _isSceneLoading;

		public void Setup()
		{
			_isSceneLoading = transform.GetComponentInParent<InGameScene>()._isSceneLoading;
			_bgmVolume.value = StaticSettingsData._bgmVolume;
			_effectVolume.value = StaticSettingsData._effectVolume;
			_autoSave.isOn = StaticSettingsData._isAutoSave;
			Time.timeScale = 0.0f;
		}

		public void ClosePopup()
		{
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
		}

		public void ChangeEffectVolume()
		{
			StaticSettingsData._effectVolume = _effectVolume.value;
		}

		public void TurnOnAutoSave()
		{
			if (_autoSave == true && StaticSettingsData._isAutoSave == false)
			{
				PopupContainer.PushPopup(_autoSaveAskingPopup.GetComponent<IPopup>());
			}

			StaticSettingsData._isAutoSave = _autoSave.isOn;
		}

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
			Time.timeScale = 1.0f;
			_isSceneLoading = true;
			SceneChangerSingleton._instance.ChangeSceneWithFadeOut(TITLE_SCENE_PATH);
		}
	}
}

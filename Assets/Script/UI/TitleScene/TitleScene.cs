using NHD.UI.Common;
using NHD.Utils.SceneUtil;
using NHD.Utils.SoundUtil;
using UnityEngine;

namespace NHD.UI.titleScene
{
    public class TitleScene : MonoBehaviour
    {
        //testing
        private const string GAME_SCENE_NAME = "OpeningScene";

        [SerializeField] private GameObject _historyPopup;
        [SerializeField] private GameObject _creditPopup;
        [SerializeField] private GameObject _settingsPopup;
        [SerializeField] private GameObject _exitAskingPopup;
        [SerializeField] private AudioClip _clickSound;
        private bool _isSceneLoading;

		private void OnEnable()
		{
            _isSceneLoading = false;
		}

        private void Start()
        {
            SoundManager._instance.PlayRandomBGM();
        }
        private void Update()
        {
            if (!_isSceneLoading)
            {
                CheckInputKey();
            }
        }

        private void CheckInputKey()
		{
            OnInputEscKey();
		}

        private void OnInputEscKey()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (PopupContainer._popupContainer.Count == 0)
                {
                    OpenExitAskingPopup();
                }
                else
                {
                    PopupContainer.PopPopup();
                }
            }
        }

        public void GameStart()
        {
            PlayClickSound();
            _isSceneLoading = true;
            SceneChangerSingleton._instance.ChangeSceneWithFadeOut(GAME_SCENE_NAME);
        }

        public void OpenHistoryPopup()
        {
            PlayClickSound();
            PopupContainer.PushPopup(_historyPopup.GetComponent<IPopup>());
        }

        public void OpenCreditPopup()
        {
            PlayClickSound();
            PopupContainer.PushPopup(_creditPopup.GetComponent<IPopup>());
        }

        public void OpenSettingsPopup()
        {
            PlayClickSound();
            PopupContainer.PushPopup(_settingsPopup.GetComponent<IPopup>());
        }

        public void OpenExitAskingPopup()
        {
            PlayClickSound();
            PopupContainer.PushPopup(_exitAskingPopup.GetComponent<IPopup>());
        }

        public void PlayClickSound()
        {
            SoundManager._instance.PlayEFXAmbient(_clickSound);
        }
    }
}
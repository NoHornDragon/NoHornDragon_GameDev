using NHD.UI.Common;
using NHD.Utils.SceneUtil;
using UnityEngine;

namespace NHD.UI.titleScene
{
    public class TitleScene : MonoBehaviour
    {
        private const string GAME_SCENE_NAME = "SingleGamePlay";

        [SerializeField] private GameObject _historyPopup;
        [SerializeField] private GameObject _settingsPopup;
        [SerializeField] private GameObject _exitAskingPopup;
        private bool _isSceneLoading;

		private void OnEnable()
		{
            _isSceneLoading = false;
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
            _isSceneLoading = true;
            SceneChangerSingleton._instance.ChangeSceneWithFadeOut(GAME_SCENE_NAME);
        }

        public void OpenHistoryPopup()
        {
            PopupContainer.PushPopup(_historyPopup.GetComponent<IPopup>());
        }

        public void OpenSettingsPopup()
        {
            PopupContainer.PushPopup(_settingsPopup.GetComponent<IPopup>());
        }

        public void OpenExitAskingPopup()
        {
            PopupContainer.PushPopup(_exitAskingPopup.GetComponent<IPopup>());
        }
    }
}
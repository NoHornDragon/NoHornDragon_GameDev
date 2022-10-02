using NHD.UI.Common;
using NHD.Utils.SceneUtil;
using UnityEngine;

namespace NHD.UI.titleScene
{
    public class TitleScene : MonoBehaviour
    {
        private const string GAME_SCENE_NAME = "Dev_husk";

        [SerializeField] private GameObject _historyPopup;
        [SerializeField] private GameObject _settingsPopup;
        [SerializeField] private GameObject _exitAskingPopup;

        private void Update()
        {
            OnInputEscKey();
        }

        private void OnInputEscKey()
        {
            if (Input.GetKeyUp(KeyCode.Escape) && PopupContainer._popupCount == 0)
            {
                OpenExitAskingPopup();
            }
        }

        public void GameStart()
        {
            SceneChanger._instance.ChangeScene(GAME_SCENE_NAME);
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
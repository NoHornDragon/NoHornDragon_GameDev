using NHD.DataController.Loaders;
using NHD.UI.Common;
using UnityEngine;

namespace NHD.UI.titleScene.settingsPopup.resetAskingPopup
{
    public class ResetAskingPopup : MonoBehaviour, IPopup
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

        public void ResetGameData()
        {
            PlayHistoryLoader playHistoryLoader = new PlayHistoryLoader();
            playHistoryLoader.InitializeData();
            playHistoryLoader.LoadData();
            ClosePopup();
        }
    }
}

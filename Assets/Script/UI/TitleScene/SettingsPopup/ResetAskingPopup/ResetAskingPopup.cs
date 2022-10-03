using NHD.DataController.Loaders;
using NHD.UI.Common;
using UnityEngine;

namespace NHD.UI.titleScene.settingsPopup.resetAskingPopup
{
    public class ResetAskingPopup : MonoBehaviour, IPopup
    {
        public void Setup()
        {
            this.gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
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

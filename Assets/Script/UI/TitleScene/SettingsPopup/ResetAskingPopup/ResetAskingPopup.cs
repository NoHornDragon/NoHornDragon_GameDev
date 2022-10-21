using NHD.DataController.Loaders;
using NHD.UI.Common;
using NHD.Utils.SoundUtil;
using UnityEngine;

namespace NHD.UI.titleScene.settingsPopup.resetAskingPopup
{
    public class ResetAskingPopup : MonoBehaviour, IPopup
    {
        [SerializeField] private AudioClip _closeSound;

        public void Setup()
        {
            this.gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
            SoundManager._instance.PlayEFXAmbient(_closeSound);
            this.gameObject.SetActive(false);
        }

        public void ResetGameData()
        {
            SoundManager._instance.PlayEFXAmbient(_closeSound);
            PlayHistoryLoader playHistoryLoader = new PlayHistoryLoader();
            playHistoryLoader.InitializeData();
            playHistoryLoader.LoadData();
            ClosePopup();
        }
    }
}

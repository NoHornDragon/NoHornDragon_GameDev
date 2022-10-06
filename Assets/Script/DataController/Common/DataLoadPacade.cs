using NHD.DataController.Loaders;
using UnityEngine;

namespace NHD.DataController.Common
{
    public class DataLoadPacade : MonoBehaviour
    {
        private SettingsDataLoader _settingsDataLoader = new SettingsDataLoader();
        private PlayHistoryLoader _playHistoryLoader = new PlayHistoryLoader();

        private void Awake()
        {
            _settingsDataLoader.SetupData();
            _playHistoryLoader.SetupData();
            UIStringLoader.SetupData();
            PaperDataLoader.SetupData();
            NPCCommentsDataLoader.SetupData();
        }
    }
}
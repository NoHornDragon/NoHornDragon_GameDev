using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace NHD.Utils.TimeLineUtil
{
    public class OpeningCutScene : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _playableDirector;
        private bool _skipped = false;
        [SerializeField]
        private string _GameSceneName;

        public void SkipButtonPressed(float skipTime)
        {
            if (_skipped) return;

            _playableDirector.time = skipTime;
            _skipped = true;
        }

        public void AfterTimeline()
        {
            SceneManager.LoadSceneAsync(_GameSceneName);
        }
    }
}
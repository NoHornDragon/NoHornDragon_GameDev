using NHD.Utils.SceneUtil;
using UnityEngine;
using UnityEngine.Playables;

namespace NHD.Utils.TimeLineUtil
{
    public class OpeningCutScene : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _playableDirector;
        private bool _skipped = false;
        [SerializeField]
        private string _gameSceneName;

        public void SkipButtonPressed(float skipTime)
        {
            if (_skipped) return;

            _playableDirector.time = skipTime;
            _skipped = true;
        }

        public void AfterTimeline()
        {
            SceneChangerSingleton._instance.ChangeSceneWithFadeOut(_gameSceneName);
        }
    }
}
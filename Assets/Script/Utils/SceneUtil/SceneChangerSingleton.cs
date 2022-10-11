using DG.Tweening;
using NHD.UI.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NHD.Utils.SceneUtil
{
	public class SceneChangerSingleton : MonoBehaviour
    {
        private const float FADE_DURATION = 1.0f;

        public static SceneChangerSingleton _instance;

        [SerializeField] private Image _fadeImage;
        private string _sceneName;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            FadeIn();
        }

        private void FadeIn()
        {
            _fadeImage.gameObject.SetActive(true);
            _fadeImage.DOFade(0.0f, FADE_DURATION).OnComplete(TurnOffFadeImage);
        }

        public void FadeOutAndIn()
        {
            _fadeImage.gameObject.SetActive(true);
            _fadeImage.DOFade(1.0f, FADE_DURATION).
                OnComplete(delegate () { _fadeImage.DOFade(0.0f, FADE_DURATION).OnComplete(TurnOffFadeImage); });
        }

        private void TurnOffFadeImage()
        {
            _fadeImage.gameObject.SetActive(false);
        }

        public void ChangeSceneWithFadeOut(string sceneName)
        {
            _sceneName = sceneName;
            _fadeImage.gameObject.SetActive(true);
            _fadeImage.color = Vector4.zero;
            _fadeImage.DOFade(1.0f, FADE_DURATION).OnComplete(TurnOffFadeImage).OnComplete(LoadScene);
        }

        private void LoadScene()
		{
            PopupContainer.ClearStack();
            StartCoroutine(LoadSceneCoroutine());
		}

        IEnumerator LoadSceneCoroutine()
		{
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName);

			while (!asyncLoad.isDone)
			{
                yield return null;
			}
            FadeIn();
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NHD.Utils.SceneUtil
{
    public class SceneChanger : MonoBehaviour
    {
        public static SceneChanger _instance;

        [SerializeField] private Image _fadeImage;

        private Vector4 _destColor = new Vector4();
        private float _tempA;

        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }

        public void Start()
        {
            FadeIn();
        }

        public void FadeIn()
        {
            _fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeInCoroutine());
        }

        IEnumerator FadeInCoroutine()
        {
            _tempA = 1.0f;

            for (int i = 0; i < 50; i++)
            {
                _tempA -= 0.02f;
                _destColor.x = 0;
                _destColor.y = 0;
                _destColor.z = 0;
                _destColor.w = _tempA;
                _fadeImage.color = _destColor;
                yield return null;
            }

            _destColor.x = 0;
            _destColor.y = 0;
            _destColor.z = 0;
            _destColor.w = 0;
            _fadeImage.color = _destColor;
            _fadeImage.gameObject.SetActive(false);
        }

        public void ChangeScene(string sceneName)
        {
            _fadeImage.gameObject.SetActive(true);
            StartCoroutine(ChangeSceneCoroutine(sceneName));
        }

        IEnumerator ChangeSceneCoroutine(string sceneName) // 페이드아웃과 씬 로드가 끝나면 페이드인
        {
            _tempA = 0.0f;

            for (int i = 0; i < 50; i++)
            {
                _tempA += 0.02f;
                _destColor.x = 0;
                _destColor.y = 0;
                _destColor.z = 0;
                _destColor.w = _tempA;
                _fadeImage.color = new Vector4(0, 0, 0, _tempA);
                yield return null;
            }

            _destColor.x = 0;
            _destColor.y = 0;
            _destColor.z = 0;
            _destColor.w = 1;
            _fadeImage.color = _destColor;
            _fadeImage.gameObject.SetActive(false);
            SceneManager.LoadScene(sceneName);

            FadeIn();
        }

    }
}
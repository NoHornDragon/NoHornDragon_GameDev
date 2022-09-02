using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    [SerializeField]
    private Image fadeImage;


    private float tempA;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        tempA = 1.0f;
        for(int i = 0; i < 50; i++)
        {
            tempA -= 0.02f;
            fadeImage.color = new Vector4(0, 0, 0, tempA);
            yield return null;
        }
        fadeImage.color = new Vector4(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);
    }

    public void ChangeScene(string _sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(ChangeSceneCoroutine(_sceneName));
    }

    IEnumerator ChangeSceneCoroutine(string _sceneName) // 페이드아웃과 씬 로드가 끝나면 페이드인
    {
        tempA = 0.0f;
        for (int i = 0; i < 50; i++)
        {
            tempA += 0.02f;
            fadeImage.color = new Vector4(0, 0, 0, tempA);
            yield return null;
        }
        fadeImage.color = new Vector4(0, 0, 0, 1);
        fadeImage.gameObject.SetActive(false);
        SceneManager.LoadScene(_sceneName);

        FadeIn();
    }

}

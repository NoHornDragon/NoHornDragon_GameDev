using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuButtonManager : MonoBehaviour
{
    public event Action<bool> menuButtonEvent;
    [SerializeField]
    private GameObject pausePanel;
    private bool isPausePanelOpen = false;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPausePanelOpen)
            PausePanelClose();
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPausePanelOpen)
            PausePanelOpen();
    }

    public void PausePanelOpen()
    {
        isPausePanelOpen = true;

        Time.timeScale = 0.0f;
        menuButtonEvent?.Invoke(false);

        pausePanel.SetActive(true);
    }

    public void PausePanelClose()
    {
        isPausePanelOpen = false;

        Time.timeScale = 1.0f;
        menuButtonEvent?.Invoke(true);

        pausePanel.SetActive(false);
    }

    public void GameQuit()
    {
        Time.timeScale = 1.0f;
        HistoryDataManager.instance.SaveHistoryData();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void GoToLobbyYes()
    {
        Time.timeScale = 1.0f;
        HistoryDataManager.instance.SaveHistoryData();
        SceneChanger.instance.ChangeScene("LobbyScene");
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyButtonManager : MonoBehaviour
{
    public string gameSceneName;

    [SerializeField]
    private GameObject historyPanel;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private GameObject exitPanel;

    private bool isOnePanelOpen = false;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOnePanelOpen && !HistoryManager.isDescriptionOpen && !HistoryManager.isDescriptionMoving)
            AllPanelClose();
        else if (Input.GetKeyDown(KeyCode.Escape) && !isOnePanelOpen && !HistoryManager.isDescriptionOpen && !HistoryManager.isDescriptionMoving)
            ExitPanelOpen();
    }
    public void historyPanelOpen()
    {
        if (!isOnePanelOpen)
        {
            isOnePanelOpen = true;
            historyPanel.SetActive(true);
        }
    }

    public void settingsPanelOpen()
    {
        if (!isOnePanelOpen)
        {
            isOnePanelOpen = true;
            settingsPanel.SetActive(true);
        }
    }

    public void ExitPanelOpen()
    {
        if (!isOnePanelOpen)
        {
            isOnePanelOpen = true;
            exitPanel.SetActive(true);
        }
    }

    public void AllPanelClose()
    {
        isOnePanelOpen = false;

        historyPanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneChanger.instance.ChangeScene(gameSceneName);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
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
        pausePanel.SetActive(true);
    }

    public void PausePanelClose()
    {
        isPausePanelOpen = false;
        pausePanel.SetActive(false);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void GoToLobbyYes()
    {
        SceneChanger.instance.ChangeScene("LobbyScene");
    }
}

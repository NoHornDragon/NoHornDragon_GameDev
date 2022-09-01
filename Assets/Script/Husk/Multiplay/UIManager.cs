using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;
    public InputField userNameField;
    public InputField ipAddressField;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Debug.Log($"indtance already exist, so destroy objec!");
            Destroy(this);
        }
    }

    public void ConnectToServer(bool isHost)
    {
        startMenu.SetActive(false);
        userNameField.interactable = false;

        if(!isHost)
        {
            Debug.Log($"Client build!");
            ipAddressField.interactable = false;
            Client.instance.ip = ipAddressField.text;
        }

        Client.instance.ConnectToServer();
    }
}

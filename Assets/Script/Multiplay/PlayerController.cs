using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform Player;
    private void FixedUpdate()
    {
        SendPositionToServer();
    }

    private void SendPositionToServer()
    {
        Vector3 position = Player.position;
        
        // Debug.Log($"send local position {position}");
        ClientSend.PlayerMovement(position);
    }
}

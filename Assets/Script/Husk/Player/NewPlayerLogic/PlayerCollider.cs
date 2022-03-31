﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollider : MonoBehaviour
{
    public event Action<bool> playerStunEvent;
    public event Action<bool> playerChangeEvent;
    private PlayerMovement player;
    private bool isOrigin;
    private AnotherMovement anotherMovement;
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        
        isOrigin = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            //TODO :  another movement일때 처리 동작
            // if now not origin movement, first change to origin player
            PlayerChanged(true);
            if(playerStunEvent != null)
                playerStunEvent(false);
        }
        if(other.CompareTag("AnotherMovement"))
        {
            // change player
            PlayerChanged(!isOrigin, other.gameObject.GetComponent<AnotherMovement>());
        }
    }

    private void FixedUpdate()
    {
        if(isOrigin)                return;
        if(anotherMovement == null) return;

        // if now playing with not originmovement, player collider will follow anothermovement
        this.transform.position = anotherMovement.transform.position;
    }

    private void PlayerChanged(bool isOrigin, AnotherMovement newPlayer = null)
    {
        
        this.isOrigin = isOrigin;
        anotherMovement = (isOrigin) ? null : newPlayer;

        if(playerChangeEvent != null)
            playerChangeEvent(isOrigin);
        
    }

}

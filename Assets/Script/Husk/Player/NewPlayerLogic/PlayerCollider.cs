using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollider : MonoBehaviour
{
    public event Action playerStunEvent;
    public event Action<bool> playerChangeEvent;
    public PlayerMovement player;
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
            player.PlayerStuned();
        }
        if(other.CompareTag("AnotherMovement"))
        {
            // 플레이어 조작, 비주얼 변경
            PlayerChanged(!isOrigin, other.gameObject.GetComponent<AnotherMovement>());
        }
    }

    private void FixedUpdate()
    {
        if(isOrigin)                return;
        if(anotherMovement == null) return;

        this.transform.position = anotherMovement.transform.position;
    }

    private void PlayerChanged(bool isOrigin, AnotherMovement newPlayer)
    {
        /*
        - 기존 움직임 할 수 없게, 보이지 않게
        - 여의주 스프라이트 끄기, 발사할 수 없게
        ? 플레이어가 따라가서 이 콜라이더를 계속 쓴다면?
        */
        
        this.isOrigin = isOrigin;
        anotherMovement = (isOrigin) ? null : newPlayer;

        if(playerChangeEvent != null)
            playerChangeEvent(isOrigin);
        
    }

}

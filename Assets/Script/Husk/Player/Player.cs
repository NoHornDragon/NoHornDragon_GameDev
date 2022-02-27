using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    private Rigidbody2D rigid;
    private float horizontal_speed;
    private Vector2 spwanPoint;
    [SerializeField] private float swingPower;
    [SerializeField] private bool jointNow;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        FindObjectOfType<YeouijuReflect>().CollisionEvent += PlayerCanSwing;
        FindObjectOfType<Yeouiju>().DisjointAction += PlayerCannotSwing;

        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;
    }

    private void Update() 
    {
        horizontal_speed = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.R))
            PlayerReset();
    }

    private void FixedUpdate() 
    {
        // 플레이어가 줄이 연결된게 아니라면 return
        if(!jointNow)
            return;

        rigid.AddForce(Vector2.right * horizontal_speed * swingPower);
    }

    private void PlayerCanSwing(Vector2 dummyInput)
    {
        jointNow = true;
    }
    private void PlayerCannotSwing()
    {
        jointNow = false;
    }

    private void PlayerReset()
    {
        // TODO : 아래 멘트 인게임에 추가
        /*
        혹시나 저희 게임의 버그로 인해 이 버튼을 누르셨다면 정말 죄송합니다. 버그를 제보해주시면 감사합니다.
        아님 실수로 누르셨다면... 그건 좀 안타깝군요.
        */
        this.gameObject.transform.position = spwanPoint;
    }

    #region GameState
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;
    }

    #endregion
}

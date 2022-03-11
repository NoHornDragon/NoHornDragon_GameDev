using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    private Rigidbody2D rigid;
    private float horizontal_speed;
    private Vector2 spwanPoint;
    [Header("플레이어 능력치 변수")]
    [SerializeField] private float swingPower;
    [SerializeField] private bool jointNow;
    private PlayerAnimation anim;

    [Header("플레이어 상태 변수")]
    public bool prepareLaunch;
    public bool throwYeouiju;
    public bool nowSwing;
    [SerializeField] private bool CanMove;
    public bool stuned;
    [Space]
    public bool onGround;
    [SerializeField] private Vector2 bottomOffset;
    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        CanMove = true;
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<PlayerAnimation>();

        FindObjectOfType<YeouijuReflect>().CollisionEvent += PlayerCanSwing;
        FindObjectOfType<Yeouiju>().DisjointAction += PlayerCannotSwing;
        FindObjectOfType<PlayerCollision>().ControlEvent += SetplayerMove;
    }

    private void Update() 
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        horizontal_speed = Input.GetAxis("Horizontal");

        // animation flip
        anim.FlipX(rigid.velocity.x > 0);

        if(Input.GetKeyDown(KeyCode.R))
            PlayerReset();

        if(!CanMove)    return;

        // yeouiju launch
        if(Input.GetMouseButtonDown(0)) 
            prepareLaunch = true;
        if(Input.GetMouseButtonUp(0) && prepareLaunch) 
            throwYeouiju = true;
    }



    private void FixedUpdate() 
    {
        // 플레이어가 줄이 연결된게 아니라면 return
        if(!jointNow)   return;

        rigid.AddForce(Vector2.right * horizontal_speed * swingPower);
    }

    private void PlayerCanSwing(Vector2 dummyInput)
    {
        jointNow = true;
        nowSwing = true;

        prepareLaunch = false;
        throwYeouiju = false;
    }
    private void PlayerCannotSwing()
    {
        jointNow = false;
        nowSwing = false;

        prepareLaunch = false;
        throwYeouiju = false;
    }

    public void SetplayerMove(bool input)
    {
        CanMove = input;
    }

    public void playerStuned()
    {
        CanMove = false;
        // anim
        stuned = true;
        StartCoroutine(CheckOnGround());
    }

    IEnumerator CheckOnGround()
    {
        yield return new WaitForSeconds(1f);

        if(onGround)
            PlayerRevive();
        else
            StartCoroutine(CheckOnGround());
    }

    private void PlayerRevive()
    {
        CanMove = true;
        stuned = false;
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

    // TODO : 디버그용임
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
    }
}

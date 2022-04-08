using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    public event Action<bool> PlayerRecoverEvent;
    public event Action playerResetEvent;
    // another component
    private Rigidbody2D rigid;
    private PlayerGrapher grapher;
    private YeouijuLaunch launch;
    
    [Header("플레이어 능력치")]
    private float horizontalSpeed;
    [SerializeField] private float swingPower;

    [Header("플레이어 현재 상태")]
    private bool usingEasyMode;
    [SerializeField] private bool canMove;
    public bool stuned;
    public bool nowJoint;
    public bool prepareLaunch;
    public bool throwYeouiju;
    
    [Space(20f)]

    public bool onGround;
    [SerializeField] private Vector2 bottomOffset;
    [SerializeField] private float collisionRadius;
    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        // init variable
        canMove = true;

        rigid = GetComponent<Rigidbody2D>();
        grapher = GetComponent<PlayerGrapher>();
        launch = GetComponent<YeouijuLaunch>();

        FindObjectOfType<YeouijuReflection>().collisionEvent += MakeJoint;
        PlayerCollider playerCollider = FindObjectOfType<PlayerCollider>();
        playerCollider.playerStunEvent += PlayerStuned;
        playerCollider.playerChangeEvent += PlayerBecomeOrigin;
        FindObjectOfType<YeouijuLaunch>().disJointEvent += DeleteJoint;

        usingEasyMode = SaveData.instance.userData.UseEasyMode;
        if(usingEasyMode)
        {
            this.transform.position = SaveData.instance.userData.PlayerPos;
            StartCoroutine(SavePlayerPosition());
        }
    }

    void Update()
    {
        // Restart Game
        if(Input.GetKeyDown(KeyCode.R))
        {
            // TODO : 다른 오브젝트일 때 R을 누른다면?
            PlayerReset();
            return;
        }

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        // can't move => just return
        if(!canMove)    return;

        // if(rigid.velocity.x < 0)
        //     transform.localScale = new Vector3(-1, 1, 1);
        // else 
        //     transform.localScale = new Vector3(1, 1, 1);


        horizontalSpeed = Input.GetAxis("Horizontal");

        // yeouiju launch
        if(Input.GetMouseButtonDown(0))
            prepareLaunch = true;
        if(Input.GetMouseButtonUp(0) && prepareLaunch)
            throwYeouiju = true;
    }

    private void FixedUpdate()
    {
        if(grapher.NowJoint() == false)
            return;
        
        rigid.AddForce(Vector2.right * horizontalSpeed * swingPower);
    }

    private void PlayerReset()
    {
        // if now anothermovement, change to original
        playerResetEvent?.Invoke();

        if(usingEasyMode)
        {
            // if using easymode, respawn at savepoint
            this.gameObject.transform.position = SaveData.instance.userData.PlayerPos;
            SaveData.instance.userData.resetCount++;
            return;
        }

        // if hardmode, respawn at start point
        this.gameObject.transform.position = Vector3.zero;
        SaveData.instance.userData.resetCount++;
        // TODO : 아래 멘트 인게임에 추가
        /*
        혹시나 저희 게임의 버그로 인해 이 버튼을 누르셨다면 정말 죄송합니다. 버그를 제보해주시면 감사합니다.
        아님 실수로 누르셨다면... 그건 좀 안타깝군요.
        */
    }

    private void MakeJoint(Vector2 dummyInput)
    {
        nowJoint = true;
    }

    private void DeleteJoint()
    {
        nowJoint = false;
        prepareLaunch = false;
        throwYeouiju = false;
    }

    // TODO : 디버그용임
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
    }

    public bool PlayerFlip()
    {
        return rigid.velocity.x > 0;
    }

    // is using easy mode, save player's position
    WaitForSeconds saveCycle = new WaitForSeconds(10f);
    IEnumerator SavePlayerPosition()
    {
        yield return saveCycle;

        if(onGround)
        {
            SaveData.instance.userData.PlayerPos = this.transform.position;
            SaveData.instance.SaveGame();
        }

        StartCoroutine(SavePlayerPosition());
    }

    public void PlayerStuned(bool isStuned)
    {

        canMove = false;
        stuned = true;

        StartCoroutine(PlayerRecoverFromStun());
    }

    WaitForSeconds stunRecoverCheck = new WaitForSeconds(2f);
    IEnumerator PlayerRecoverFromStun()
    {
        bool nowOnGround = onGround;
        yield return stunRecoverCheck;

        if(nowOnGround && nowOnGround == onGround)
        {
            stuned = false;
            canMove = true;

            PlayerRecoverEvent?.Invoke(true);
        }
        else 
            StartCoroutine(PlayerRecoverFromStun());
    }

    public void PlayerBecomeOrigin(bool isOrigin)
    {
        canMove = isOrigin;
        rigid.gravityScale = (isOrigin) ? 1f : 0f;

    }
}

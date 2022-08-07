using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    public event Action<bool> PlayerRecoverEvent;
    public event Action<bool> PlayerResetEvent;
    public event Action playerResetEvent;
    // another component
    private Rigidbody2D rigid;
    private PlayerGrapher grapher;
    private YeouijuLaunch launch;
    private PlayerCollider coll;
    
    [Header("플레이어 능력치")]
    private float horizontalSpeed;
    [SerializeField] private float swingPower;

    [Header("플레이어 현재 상태")]
    private bool usingEasyMode;
    [SerializeField] private bool canMove;

    [Space(20f)]
    public bool nowJoint;
    public bool throwed;
    public bool prepareLaunch;
    public bool throwYeouiju;

    [Space(20f)]
    public bool stuned;
    [SerializeField] private float stunedTime;
    private float stunTimer;
    
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
        coll = GetComponent<PlayerCollider>();

        FindObjectOfType<YeouijuReflection>().collisionEvent += MakeJoint;
        coll.playerStunEvent += PlayerStuned;
        coll.playerChangeEvent += PlayerBecomeOrigin;
        FindObjectOfType<YeouijuLaunch>().disJointEvent += DeleteJoint;

        // usingEasyMode = SaveData.instance.userData.UseEasyMode;
        usingEasyMode = SettingsManager.instance.UseEasyMode;
        // if(usingEasyMode)
        // {
        //     this.transform.position = SaveData.instance.userData.PlayerPos;
        //     StartCoroutine(SavePlayerPosition());
        // }
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

        // stun
        if(!onGround && !nowJoint)
        {
            stunTimer += Time.deltaTime;
            if(stunTimer > stunedTime)
            {
                coll.PlayerStunEvent();
            }
        }
        else
        {
            stunTimer = 0;
        }

        // can't move => just return
        if(!canMove)    return;

        horizontalSpeed = Input.GetAxis("Horizontal");

        // yeouiju launch
        if(Input.GetMouseButtonDown(0))
        {
            prepareLaunch = true;
        }
        if(Input.GetMouseButtonUp(0) && prepareLaunch)
        {
            prepareLaunch = false;
            throwYeouiju = true;
            throwed = !throwed;
        }
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

        // if(usingEasyMode)
        // {
        //     // if using easymode, respawn at savepoint
        //     this.gameObject.transform.position = SaveData.instance.userData.PlayerPos;
        //     SaveData.instance.userData.resetCount++;
        //     return;
        // }

        // if hardmode, respawn at start point
        this.gameObject.transform.position = Vector3.zero;
        // SaveData.instance.userData.resetCount++;
        
        // TODO : 출시 전에는 이거 추가해야 함
        // HistoryDataManager.instance.AddRestartCount(1);

        PlayerResetEvent(true);
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
        throwed = false;
    }

    // TODO : For debug
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
    }

    public bool PlayerFlip()
    {
        return rigid.velocity.x > 0;
    }

    // Now using easy mode, save player's position in 10s
    // WaitForSeconds saveCycle = new WaitForSeconds(10f);
    // IEnumerator SavePlayerPosition()
    // {
    //     yield return saveCycle;

    //     if(onGround)
    //     {
    //         SaveData.instance.userData.PlayerPos = this.transform.position;
    //         SaveData.instance.SaveGame();
    //     }

    //     StartCoroutine(SavePlayerPosition());
    // }

    public void PlayerStuned(bool isStuned)
    {
        canMove = !isStuned;
        stuned = isStuned;
        SoundManager.instance.PlayRandomBGM();

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

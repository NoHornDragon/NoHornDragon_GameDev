using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // another component
    private Rigidbody2D rigid;
    private PlayerGrapher grapher;
    private YeouijuLaunch launch;

    
    [Header("플레이어 능력치")]
    private float horizontalSpeed;
    [SerializeField] private float swingPower;

    [Header("플레이어 현재 상태")]
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

        FindObjectOfType<YeouijuReflection>().CollisionEvent += MakeJoint;
        FindObjectOfType<YeouijuReflection>().YeouijuReturnEvent += DeleteJoint;
        FindObjectOfType<YeouijuLaunch>().DisJointEvent += DeleteJoint;
        FindObjectOfType<PlayerCollider>().playerStunEvent += PlayerStuned;

        if(SaveData.instance.userData.nowUseSave())
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
            PlayerReset();
            return;
        }

        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        // animation flip

        // can't move => just return
        if(!canMove)    return;

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
        // TODO : 아래 멘트 인게임에 추가
        /*
        혹시나 저희 게임의 버그로 인해 이 버튼을 누르셨다면 정말 죄송합니다. 버그를 제보해주시면 감사합니다.
        아님 실수로 누르셨다면... 그건 좀 안타깝군요.
        */
        this.gameObject.transform.position = Vector3.zero;
        SaveData.instance.userData.resetCount++;
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

    WaitForSeconds saveCycle = new WaitForSeconds(10f);
    IEnumerator SavePlayerPosition()
    {
        yield return saveCycle;

        SaveData.instance.userData.PlayerPos = this.transform.position;
        SaveData.instance.SaveGame();

        StartCoroutine(SavePlayerPosition());
    }

    // TODO 플레이어 스턴 함수
    private void PlayerStuned()
    {
        Debug.Log("플레이어 스턴 당함");
    }
}

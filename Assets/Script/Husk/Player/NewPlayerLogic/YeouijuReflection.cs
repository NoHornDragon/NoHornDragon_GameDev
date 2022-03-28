using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class YeouijuReflection : MonoBehaviour
{
    public event Action<Vector2> CollisionEvent;
    public event Action YeouijuReturnEvent;
    private Rigidbody2D rigid;
    private CircleCollider2D coll;
    private Transform player;
    private SpriteRenderer sprite;
    [SerializeField] private float yeouijuSpeed;
    private bool yeouijuOn;
    private Vector3 prevVelocity;

    [Header("충돌 변수들")]
    [SerializeField] private int collisionCount;
    [SerializeField] private float maxDistance;
    [SerializeField] private int reflectCount;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();

        FindObjectOfType<YeouijuLaunch>().DisJointEvent += YeouijuFollowPlayer;
        FindObjectOfType<PlayerCollider>().playerChangeEvent += SetYeouijuSprite;
    }

    private void FixedUpdate()
    {
        if(yeouijuOn)
        {
            prevVelocity = rigid.velocity;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, yeouijuSpeed * 3f * Time.deltaTime);
    }

    public void Launched(Vector3 position, float rotation)
    {
        reflectCount = 0;

        yeouijuOn = true;
        this.transform.position = position;
        this.transform.rotation = Quaternion.Euler(0, 0, rotation);

        rigid.velocity = transform.right * yeouijuSpeed;
        coll.enabled = true;
    }

    public void YeouijuFollowPlayer()
    {
        yeouijuOn = false;
        coll.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 아직 덜 튕겼을 시 카운트++ 한 뒤 다음 방향으로
        if(reflectCount < collisionCount)
        {
            reflectCount++;

            float speed = prevVelocity.magnitude;
            Vector2 Direction = Vector2.Reflect(prevVelocity.normalized, other.contacts[0].normal);

            rigid.velocity = Direction * Mathf.Max(speed, 0f);
            return;
        }

        // 충돌은 다 되었으나 거리가 너무 멀 때
        if(Vector2.Distance(player.position, this.transform.position) > maxDistance)
        {
            reflectCount = 0;
            
            if(YeouijuReturnEvent != null)
                YeouijuReturnEvent();

            YeouijuFollowPlayer();
            return;
        }
        
        // 충돌이 다 되었고 조건도 만족했을 때 속도를 멈춘 뒤 joint event 실행
        if(CollisionEvent != null)
            CollisionEvent(this.transform.position);
        
        rigid.velocity = new Vector3(0, 0, 0);
        rigid.freezeRotation = true;
    }

    public void SetYeouijuSprite(bool isActive)
    {
        sprite.enabled = isActive;
    }
}

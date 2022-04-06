using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class YeouijuReflection : MonoBehaviour
{
    public event Action<Vector2> collisionEvent;
    public event Action yeouijuReturnEvent;
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
        sprite = GetComponent<SpriteRenderer>();

        FindObjectOfType<YeouijuLaunch>().disJointEvent += YeouijuFollowPlayer;
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
        // still can reflect
        if(reflectCount < collisionCount)
        {
            reflectCount++;

            float speed = prevVelocity.magnitude;
            Vector2 Direction = Vector2.Reflect(prevVelocity.normalized, other.contacts[0].normal);

            rigid.velocity = Direction * Mathf.Max(speed, 0f);
            return;
        }

        // end to collistion but too far
        if(Vector2.Distance(player.position, this.transform.position) > maxDistance)
        {
            reflectCount = 0;
            
            if(yeouijuReturnEvent != null)
                yeouijuReturnEvent();

            YeouijuFollowPlayer();
            return;
        }
        
        // end collision -> make disjoint2d
        if(collisionEvent != null)
            collisionEvent(this.transform.position);
        
        rigid.velocity = new Vector3(0, 0, 0);
        rigid.freezeRotation = true;
    }

    public void SetYeouijuSprite(bool isActive)
    {
        sprite.enabled = isActive;
    }
}

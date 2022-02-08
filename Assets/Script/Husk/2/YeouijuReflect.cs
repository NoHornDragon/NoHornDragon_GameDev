using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YeouijuReflect : MonoBehaviour
{
    Yeouiju yeouiju;
    Transform playerPos;
    Rigidbody2D rigid;
    CircleCollider2D coll;
    Vector3 lastVelocity;
    public int collisionCount = 0;
    public event Action<Vector2> CollisionEvent;
    public int maxCollisionCount;

    private void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        yeouiju = FindObjectOfType<Yeouiju>();
    }

    private void OnCollisionEnter2D(Collision2D coll) 
    {
        if(!coll.transform.CompareTag("Player"))
        {
            if(collisionCount < maxCollisionCount)
            {
                collisionCount++;
                var speed = lastVelocity.magnitude;
                var dir = Vector2.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

                rigid.velocity = dir * Mathf.Max(speed, 0f);
            }
            else
            {
                if(CollisionEvent != null)
                    CollisionEvent(this.transform.position);
                else 
                    Debug.Log("z");
                rigid.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    private void FixedUpdate() 
    {
        lastVelocity = rigid.velocity;
        if(!yeouiju.isYeouijuOn)
        {   
            rigid.velocity = new Vector3(0, 0, 0);
            collisionCount = 0;
            coll.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, yeouiju.yeouijuSpeed * 1.5f * Time.deltaTime);
        }
        else
        {
            coll.enabled = true;
        }
    }

}
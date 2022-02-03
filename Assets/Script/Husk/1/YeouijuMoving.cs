using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YeouijuMoving : MonoBehaviour
{
    private YeouijuParabola yeouiju;
    public event Action<Vector2> MakeJoint2D;
    private Rigidbody2D rigid;
    private Transform playerPos;
    private CircleCollider2D coll;
    const int origin_gravity = 1;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        yeouiju = FindObjectOfType<YeouijuParabola>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate() 
    {
        float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Deg2Rad;
        transform.eulerAngles = new Vector3(0,0,angle);

        if(!yeouiju.launching)
        {
            coll.enabled = false;
            rigid.gravityScale = 0;
            coll.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, 5 * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StopYeouiju();

        if(MakeJoint2D != null)
            MakeJoint2D(this.transform.position);
    }

    public void LaunchYeouiju(float z, float power)
    {
        coll.enabled = true;
        rigid.gravityScale = origin_gravity;

        transform.rotation = Quaternion.Euler(0, 0, z);

        rigid.velocity = transform.right * power;
    }

    private void StopYeouiju()
    {
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0f;
        coll.enabled = false;
    }
}

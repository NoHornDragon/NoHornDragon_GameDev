using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AnotherMovement : MonoBehaviour
{
    
    Vector3 originPos;
    bool isActive;
    public event Action BecomeToOriginEvent;
    Rigidbody2D rigid;

    private void Start()
    {
        originPos = this.transform.position;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!isActive)
            return;
        
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO : 이거 태그 고려
        // if(other.CompareTag("Player"))
        // {
        //     BackToOrigin();
        // }
        if(other.CompareTag("Enemy"))
            BackToOrigin();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Room"))
        {
            BackToOrigin();
        }
    }

    public virtual void Movement(){}

    public void BackToOrigin()
    {
        isActive = false;

        gameObject.transform.position = originPos;
        
        if(BecomeToOriginEvent != null)
            BecomeToOriginEvent();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    Rigidbody2D rigid;
    private float horizontal_speed;
    public float power;
    public bool test_jump;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        horizontal_speed = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
            test_jump = true;
        

        if(test_jump)
        {   
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            test_jump = false;
        }
    }

    private void FixedUpdate() 
    {

        rigid.AddForce(Vector2.right * horizontal_speed * power);
    }
}

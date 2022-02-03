using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    Rigidbody2D rigid;
    private float horizontal_speed;
    public float power;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        horizontal_speed = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() 
    {
        rigid.AddForce(Vector2.right * horizontal_speed * power);
    }
}

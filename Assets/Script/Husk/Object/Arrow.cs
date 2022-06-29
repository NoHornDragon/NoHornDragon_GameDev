using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : FiringObject
{
    private Rigidbody2D rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public override void Fire()
    {
        rigid.velocity = new Vector2(4f, 0f);
    }
}

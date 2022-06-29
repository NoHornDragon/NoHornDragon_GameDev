using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringObject : MonoBehaviour
{
    private Rigidbody2D rigid;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public virtual void Fire()
    {

    }

    public virtual void Stop()
    {
        rigid.velocity = Vector2.zero;
    }
}

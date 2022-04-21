using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BackGroundScroller : MonoBehaviour
{
    public event Action<float, float> playerMoveEvent;
    Vector2 prevPos;
    private void Start()
    {
        prevPos = transform.position;
    }

    private void Update()
    {
        float xChangeAmount = prevPos.x - transform.position.x;
        float yChangeAmount = prevPos.y - transform.position.y;

        if(xChangeAmount != 0 || yChangeAmount != 0){
            playerMoveEvent?.Invoke(xChangeAmount, yChangeAmount);
            prevPos = transform.position;
        }
    }
}

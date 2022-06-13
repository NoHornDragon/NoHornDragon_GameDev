using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// TODO : 테스트 후 삭제
[Serializable]
public class SavePos
{
    public Vector3 pos;

    public SavePos(Vector3 newPos)
    {
        pos = newPos;
    }
}

public class BackGroundScroller : MonoBehaviour
{
    public event Action<float, float> playerMoveEvent;
    Vector2 prevPos;
    SavePos initialPos;
    public Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        initialPos = new SavePos(this.transform.position);
    }

    private void Start()
    {
        prevPos = transform.position;
    }

    private void Update()
    {
        playerMoveEvent?.Invoke(initialPos.pos.x - transform.position.x, initialPos.pos.y - transform.position.y);
    }

    public void ChangeCameraPos(uint d, bool input)
    {
        // if(!input)  return;

        initialPos = new SavePos(player.position);
        Debug.Log($"{initialPos.pos}");
    }
}

/*
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

        if(xChangeAmount != 0 || yChangeAmount != 0)
        {
            playerMoveEvent?.Invoke(xChangeAmount, yChangeAmount);
            prevPos = transform.position;
        }
    }
}
*/
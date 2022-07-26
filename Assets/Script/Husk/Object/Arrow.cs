using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : FiringObject
{
    public Transform target;
    public float speed = 5.0f;
    [SerializeField] Vector2[] path;
    [SerializeField] private int pathIndex;
    [SerializeField]
    private Vector2 moveDir;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable()
    {
        RequestAStarPath.RequestPath(transform.position, target.position, AfterFindPath);
    }

    private void AfterFindPath(Vector2[] way, bool success)
    {
        if(!success)    return;

        path = way;

        StopAllCoroutines();

        if(this.gameObject.activeInHierarchy)
            StartCoroutine("MoveToTarget");
    }


    IEnumerator MoveToTarget()
    {
        Vector2 curPos = path[0];
        moveDir = curPos - (Vector2)transform.position;
        transform.right = moveDir;

        while(true)
        {
            if((Vector2)transform.position == curPos)
            {
                pathIndex++;
                if(pathIndex >= path.Length)
                {
                    StartCoroutine(MoveToEndPoint());
                    yield break;
                }
                curPos = path[pathIndex];
                moveDir = curPos - (Vector2)transform.position;
                transform.right = moveDir;
            }

            transform.position = Vector2.MoveTowards(transform.position, curPos, speed * Time.deltaTime);
            yield return null;
        }
        
    }

    IEnumerator MoveToEndPoint()
    {
        while(true)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveDir * 100, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "Player":
                StopCoroutine("MoveToTarget");
                firePool.ReturnItem(this);
                break;
            case "Ground":
                StopCoroutine("MoveToTarget");
                firePool.ReturnItem(this);
                break;
        }

    }


    private void OnDisable()
    {
        pathIndex = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : FiringObject
{
    public Transform target;
    public float speed = 5.0f;
    [SerializeField] Vector2[] path;
    [SerializeField] private int pathIndex;

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
        StopCoroutine("MoveToTarget");

        if(this.gameObject.activeInHierarchy)
            StartCoroutine("MoveToTarget");
    }

    IEnumerator MoveToTarget()
    {
        Vector2 curPos = path[0];
        Vector2 moveDir;
        while(true)
        {
            if((Vector2)transform.position == curPos)
            {
                pathIndex++;
                if(pathIndex >= path.Length)
                {
                    yield break;
                }
                curPos = path[pathIndex];
                moveDir = curPos - (Vector2)transform.position;
            }

            transform.position = Vector2.MoveTowards(transform.position, curPos, speed * Time.deltaTime);
            yield return null;
        }

        // TODO : movetowards 끝날 때 방향을 기억해 처리
        transform.position = Vector2.MoveTowards(transform.position, moveDir, speed * Time.deltaTime);
        Debug.Log($"가는중");
    }

    private void OnDrawGizmos()
    {
        if(path == null)    return;

        for(int i = pathIndex; i < path.Length; i++)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawCube(path[i], Vector2.one);

            if(i == pathIndex)
                Gizmos.DrawLine(transform.position, path[i]);
            else
                Gizmos.DrawLine(path[i-1], path[i]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Confiner"))    return;

        Debug.Log($"{other.gameObject.name}");
        // TODO : 풀에 돌아가는 기능 구현
        StopCoroutine("MoveToTarget");
        firePool.ReturnItem(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
                Debug.Log($"{other.gameObject.name}");
        // TODO : 풀에 돌아가는 기능 구현
        StopCoroutine("MoveToTarget");
        firePool.ReturnItem(this);
    }


    private void OnDisable()
    {
        pathIndex = 0;
    }
}

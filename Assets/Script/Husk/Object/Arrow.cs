using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : FiringObject
{
    public Transform target;
    public float speed = 5.0f;
    [SerializeField] Vector2[] path;
    int pathIndex;

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
        StartCoroutine("MoveToTarget");
    }

    IEnumerator MoveToTarget()
    {
        Vector2 curPos = path[0];
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
            }

            transform.position = Vector2.MoveTowards(transform.position, curPos, speed * Time.deltaTime);
            yield return null;
        }

        // TODO : 화살 두번째 부터는 날아가다가 안 날아가는 오류 해결
        // TODO : movetowards 끝날 때 방향을 기억해 처리
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
        // TODO : 풀에 돌아가는 기능 구현
    }


    private void OnDisable()
    {
        pathIndex = 0;
    }
}

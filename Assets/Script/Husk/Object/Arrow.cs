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
        if(other.CompareTag("Room"))      return;

        Debug.Log($"trigger {other.gameObject.name}");
        // TODO : 풀에 돌아가는 기능 구현
        StopCoroutine("MoveToTarget");
        firePool.ReturnItem(this);
    }


    private void OnDisable()
    {
        pathIndex = 0;
    }
}

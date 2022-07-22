using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireingNPC : MonoBehaviour
{
    [SerializeField]
    private FirePool firePool;
    private FiringObject curFiringObject;
    private Transform player;
    [SerializeField]
    private Transform bone;
    private bool lookAtRight;
    [SerializeField]
    private float launchTime;
    [SerializeField]
    private float curTime;


    private void Start()
    {
        firePool = GetComponent<FirePool>();
        player = GameObject.FindWithTag("Player").transform;
        
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        WorldToGrid grid = GetComponent<WorldToGrid>();
        collider.size = new Vector2(grid.GridSizeX, grid.GridSizeY);
        collider.offset = grid.GridOffset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(StartAimingPlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator StartAimingPlayer()
    {
        while(true)
        {
            curTime += Time.deltaTime;
            if(curTime >= launchTime)
            {
                firePool.GetFireItem();
                curTime = 0;
            }


            var len = player.position - transform.position;
            // TODO : sprite.splitx
            if((len.x > 0) != lookAtRight)
            {
                lookAtRight = (len.x > 0);
                transform.localScale = (lookAtRight) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            }

            float angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -50, 50);
            bone.localRotation  = Quaternion.Euler(0, 0, angle);

            yield return null;
        }

    }
}

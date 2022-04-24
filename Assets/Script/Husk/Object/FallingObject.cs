using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
[Falling Enemy Script]
- Have 3 state
    - stay
    - falling
    - prepare restart
If player come under this object, this object will fall to ground. Player will stuned by this (in `PlayerCollider.cs`)
Then prepare in original position
*/
public class FallingObject : MonoBehaviour
{
    private enum ObjectState { Stay, Falling, Restarting };
    [SerializeField] private ObjectState objState = ObjectState.Stay;
    private RaycastHit2D rayHit;
    private Vector3 originPos;
    private Collider2D coll;
    private int playerLayer;
    private bool nowOnTrigger;
    private bool nowCoroutine;
    private readonly WaitForSeconds delayTime = new WaitForSeconds(2f);

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        playerLayer = 1 << LayerMask.NameToLayer("Player");
        originPos = transform.position;
    }

    private void Update()
    {
        if(nowCoroutine)   return;
        switch(objState)
        {
            case ObjectState.Stay :
                StayUpdate();
                break;

            case ObjectState.Falling :
                FallingUpdate();
                break;
            
            case ObjectState.Restarting :
                RestartingUpdate();
                break;
        }
    }

    private void StayUpdate()
    {
        rayHit = Physics2D.Raycast(transform.position, Vector2.down, 20f, playerLayer);

        if(rayHit.collider != null)
        {
            objState = ObjectState.Falling;
        }
    }

    private void FallingUpdate()
    {
        transform.Translate(Vector3.down * 10f * Time.deltaTime);

        if(nowOnTrigger)
        {
            nowOnTrigger = false;
            coll.enabled = false;
            StartCoroutine(FallingCoroutine());
        }
    }

    IEnumerator FallingCoroutine()
    {
        nowCoroutine = true;

        yield return delayTime;
        objState = ObjectState.Restarting;
        nowCoroutine = false;
    }

    private void RestartingUpdate()
    {
        transform.position = originPos;
        nowCoroutine = true;
        coll.enabled = true;
        StartCoroutine(RestartCoroutine());
    }

    IEnumerator RestartCoroutine()
    {
        yield return delayTime;

        objState = ObjectState.Stay;
        nowCoroutine = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        nowOnTrigger = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{
    public event Action<bool> ControlEvent;
    Player originMovement;
    Grapher grapher;
    GameObject originSprite;
    AnotherMovement newMovement;

    private void Start()
    {
        originMovement = GetComponent<Player>();
        grapher = GetComponent<Grapher>();

        originSprite = transform.FindChild("Visual").gameObject;

        AnotherMovement[] anothers = FindObjectsOfType<AnotherMovement>();
        for(int i = 0; i < anothers.GetLength(0); i++)
            anothers[i].BecomeToOriginEvent += ChangeToOriginMovement;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("AnotherMovement"))
        {
            Debug.Log("AnotherMovement");
            ChangeToNewMovement(other);
        }
        if(other.CompareTag("Enemy"))
        {

        }
    }

    void ChangeToNewMovement(Collider2D other) 
    {
        newMovement = other.gameObject.GetComponent<AnotherMovement>();

        if(ControlEvent != null)
            ControlEvent(false);

        originSprite.SetActive(false);
        originMovement.enabled = false;
        grapher.enabled = false;
    }

    public void ChangeToOriginMovement()
    {
        newMovement.BackToOrigin();

        if(ControlEvent != null)
            ControlEvent(true);

        originSprite.SetActive(true);
        originMovement.enabled = true;
        grapher.enabled = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{
    // connected with player move(origin), Yeouiju can launch
    public event Action<bool> ControlEvent;
    private Player originMovement;
    private Grapher grapher;
    private GameObject originSprite;
    private AnotherMovement newMovement;

    private void Start()
    {
        originMovement = GetComponent<Player>();
        grapher = GetComponent<Grapher>();

        originSprite = transform.Find("Visual").gameObject;
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
            originMovement.playerStuned();
        }
    }

    void ChangeToNewMovement(Collider2D other) 
    {
        newMovement = other.gameObject.GetComponent<AnotherMovement>();

        // Disconnect Yeouiju
        // FindObjectOfType<Yeouiju>().Disjoint();
        // FindObjectOfType<Grapher>().DeleteJoint();

        if(ControlEvent != null)
            ControlEvent(false);

        originSprite.SetActive(false);
        originMovement.enabled = false;
    }
    [ContextMenu("Player Change to Origin")]
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFarCheck : MonoBehaviour
{
    public NPC currentNPC;

    private void Awake()
    {
        currentNPC = GetComponentInParent<NPC>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            currentNPC.visitCount++;
        }
    }
}

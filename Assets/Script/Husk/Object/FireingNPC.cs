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

    private void Awake()
    {
        firePool = GetComponent<FirePool>();
        player = GameObject.FindWithTag("Player").transform;

    }

    private void Update()
    {
        var len = player.position - transform.position;
        float angle = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        bone.localRotation  = Quaternion.Euler(0, 0, angle);

        Debug.Log($"angle : {angle} / bone : {bone.rotation.z}");
        
        // if(Input.GetKeyDown(KeyCode.M))
        // {
        //     curFiringObject = firePool.GetFireItem();
        // }
    }
}

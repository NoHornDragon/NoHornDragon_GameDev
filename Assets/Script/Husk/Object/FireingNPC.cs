using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireingNPC : MonoBehaviour
{
    [SerializeField]
    private FirePool firePool;
    private FiringObject curFiringObject;

    private void Awake()
    {
        firePool = GetComponent<FirePool>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            curFiringObject = firePool.GetFireItem();
        }
        // if(Input.GetKeyDown(KeyCode.N))
        // {
        //     // TODO : 이후 돌아가는 로직을 화살에서 처리하기
        //     firePool.ReturnItem(curFiringObject);
        //     curFiringObject = null;
        // }
    }
}

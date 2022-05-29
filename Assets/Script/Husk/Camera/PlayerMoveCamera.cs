using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCamera : MonoBehaviour
{
    private PlayerMovement player;
    private Vector3 originPos = new Vector3(0, 0, 0);
    [SerializeField] private float cameraMoveAmount;

    [SerializeField] [Range(0, 0.3f)] 
    private float cameraMoveSpeed;
    private Vector3 nowInput = new Vector3(0, 0, 0);
    private bool initialized;

    private void Start()
    {
        player = transform.parent.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(!player.onGround || player.nowJoint)
        {
            SetInitialState();
            return;
        }
        
        nowInput.x = Input.GetAxis("Horizontal");
        nowInput.y = Input.GetAxis("Vertical");

        if(nowInput.x == 0 && nowInput.y == 0)
        {
            SetInitialState();
            return;
        }
        
        initialized = false;

        if(Mathf.Abs(transform.localPosition.x) > cameraMoveAmount)
            nowInput.x = 0;
        if(Mathf.Abs(transform.localPosition.y) > cameraMoveAmount)
            nowInput.y = 0;

        this.transform.localPosition += nowInput * cameraMoveSpeed;
    }

    private void SetInitialState()
    {
        if(initialized) return;

        transform.localPosition = Vector3.zero;
        nowInput = Vector3.zero;
        initialized = true;
    }
}

/*
- 입력이 있는가 -> 움직일 수 있는가? -> 벗어나진 않았는가 -> 움직인다!
- 입력이 없으면 -> 초기화
*/

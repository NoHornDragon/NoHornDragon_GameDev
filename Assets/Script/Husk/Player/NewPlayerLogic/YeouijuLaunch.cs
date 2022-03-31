using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YeouijuLaunch : MonoBehaviour
{
    public event Action disJointEvent;
    private YeouijuReflection yeouiju;
    [SerializeField] private bool canLaunch;
    public bool isYeouijuOn;
    
    private void Start()
    {
        canLaunch = true;
        
        yeouiju = FindObjectOfType<YeouijuReflection>();

        FindObjectOfType<PlayerMovement>().PlayerRecoverEvent += SetLaunchStatus;
        FindObjectOfType<PlayerCollider>().playerChangeEvent += SetLaunchStatus;
        FindObjectOfType<PlayerCollider>().playerStunEvent += StunedYeouiju;
    }

    private void Update()
    {
        if(!canLaunch)                   return;
        if(!Input.GetMouseButtonUp(0))  return;
    
        if(!isYeouijuOn)
        {
            isYeouijuOn = true;

            // 마우스 방향에 따라 오브젝트의 회전각 결정
            Vector2 len        = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float z            = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, z);

            yeouiju.Launched(this.transform.position, z);
            return;
        }

        if(disJointEvent != null)
        {
            Debug.Log("return 여의주");
            isYeouijuOn = false;
            disJointEvent();
            return;
        }
    }

    public void SetLaunchStatus(bool isActive)
    {
        this.canLaunch = isActive;
    }

    public void StunedYeouiju(bool isStuned)
    {
        canLaunch = isStuned;   
        if(disJointEvent != null)
            disJointEvent();
    }
    
}

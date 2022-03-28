using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YeouijuLaunch : MonoBehaviour
{
    public event Action DisJointEvent;
    private YeouijuReflection yeouiju;
    private bool canLaunch;
    public bool isYeouijuOn;
    private bool isActive;
    
    private void Start()
    {
        canLaunch = true;
        yeouiju = FindObjectOfType<YeouijuReflection>();

        FindObjectOfType<PlayerCollider>().playerChangeEvent += SetLaunchStatus;
    }

    private void Update()
    {
        if(!isActive)                   return;
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

        if(DisJointEvent != null)
        {
            Debug.Log("return 여의주");
            isYeouijuOn = false;
            DisJointEvent();
            return;
        }
    }

    public void SetLaunchStatus(bool isActive)
    {
        this.isActive = isActive;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YeouijuLaunch : MonoBehaviour
{
    public event Action disJointEvent;
    private YeouijuReflection yeouiju;
    [SerializeField] private bool canLaunch;
    private bool isYeouijuOn;
    private bool usingEasyMode;
    
    private void Start()
    {
        canLaunch = true;
        usingEasyMode = SaveData.instance.userData.UseEasyMode;
        
        yeouiju = FindObjectOfType<YeouijuReflection>();

        disJointEvent += SetYeouijuFalse;

        FindObjectOfType<PlayerMovement>().PlayerRecoverEvent += SetLaunchStatus;
        PlayerCollider playerCollider = FindObjectOfType<PlayerCollider>();
        playerCollider.playerChangeEvent += SetLaunchStatus;
        playerCollider.playerStunEvent += StunedYeouiju;

        FindObjectOfType<PlayerMovement>().playerResetEvent += ReturnYeouiju;
        FindObjectOfType<YeouijuReflection>().yeouijuReturnEvent += ReturnYeouiju;
        FindObjectOfType<PlayerGrapher>().deleteJointEvent += ReturnYeouiju;
    }

    private void Update()
    {
        if(!canLaunch)                   return;
        if(!Input.GetMouseButtonUp(0))  return;
    
        if(!isYeouijuOn)
        {
            isYeouijuOn = true;

            // 마우스 방향에 따라 오브젝트의 회전각 결정
            var len             = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var z               = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
            transform.rotation  = Quaternion.Euler(0, 0, z);

            yeouiju.Launched(this.transform.position, z);
            return;
        }

        ReturnYeouiju();
    }

    private void SetYeouijuFalse()
    {
        isYeouijuOn = false;
    }
    public void SetLaunchStatus(bool isActive)
    {
        this.canLaunch = isActive;
    }

    public void StunedYeouiju(bool isStuned)
    {
        canLaunch = !isStuned;   
        ReturnYeouiju();
    }

    private void ReturnYeouiju()
    {
        disJointEvent?.Invoke();
    }
    
}

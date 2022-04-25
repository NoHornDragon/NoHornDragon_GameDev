using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YeouijuLaunch : MonoBehaviour
{
    public event Action disJointEvent;
    private YeouijuReflection yeouiju;
    private LineRenderer predictionLine;
    private int predictionLayerMask;
    private RaycastHit2D predictionHit;
    [SerializeField] private bool canLaunch;
    [SerializeField] private bool prepareYeouiju;
    private bool isYeouijuOn;
    private bool usingEasyMode;
    
    private void Start()
    {
        predictionLayerMask = (1 << LayerMask.NameToLayer("Ground"));

        canLaunch = true;
        usingEasyMode = SaveData.instance.userData.UseEasyMode;
        
        yeouiju = FindObjectOfType<YeouijuReflection>();
        predictionLine = GetComponent<LineRenderer>();
        predictionLine.enabled = false;

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
        if(!canLaunch)                  return;

        predictionLine.SetPosition(0, this.transform.position);

        if(Input.GetMouseButtonDown(0))
        {
            prepareYeouiju = true;
        }

        if(!isYeouijuOn && prepareYeouiju)
        {
            var len             = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var z               = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
            transform.rotation  = Quaternion.Euler(0, 0, z);

            predictionHit = Physics2D.Raycast(this.gameObject.transform.position, transform.right, Mathf.Infinity, predictionLayerMask);

            if(predictionHit.collider != null)
            {
                predictionLine.SetPosition(1, predictionHit.point);
            }

            predictionLine.enabled = true;
        }

        if(!Input.GetMouseButtonUp(0))  return;
    
        if(!isYeouijuOn)
        {
            predictionLine.enabled = false;
            isYeouijuOn = true;

            // 마우스 방향에 따라 오브젝트의 회전각 결정
            var len             = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var z               = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
            transform.rotation  = Quaternion.Euler(0, 0, z);

            yeouiju.Launched(this.transform.position, z);
            return;
        }

        predictionLine.enabled = false;

        ReturnYeouiju();
    }


    private void DrawPredictionLine()
    {

    }


    private void SetYeouijuFalse()
    {
        isYeouijuOn = false;
        prepareYeouiju = false;
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

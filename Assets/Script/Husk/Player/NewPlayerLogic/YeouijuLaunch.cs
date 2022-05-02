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
    private RaycastHit2D predictionHit2;
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

        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.PlayerRecoverEvent += SetLaunchStatus;
        playerMovement.playerResetEvent += ReturnYeouiju;

        PlayerCollider playerCollider = FindObjectOfType<PlayerCollider>();
        playerCollider.playerChangeEvent += SetLaunchStatus;
        playerCollider.playerStunEvent += StunedYeouiju;
        
        FindObjectOfType<YeouijuReflection>().yeouijuReturnEvent += ReturnYeouiju;
        FindObjectOfType<PlayerGrapher>().deleteJointEvent += ReturnYeouiju;
    }

    private void Update()
    {
        if(!canLaunch)                  return;

        if(Input.GetMouseButtonDown(0))
        {
            prepareYeouiju = true;
        }

        // calculate angle by mouse pointer
        var len             = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var z               = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation  = Quaternion.Euler(0, 0, z);

        if(!isYeouijuOn && prepareYeouiju && usingEasyMode)
        {
            DrawPredictionLine();
        }

        if(!Input.GetMouseButtonUp(0))  return;
    
        if(!isYeouijuOn)
        {
            predictionLine.enabled = false;
            isYeouijuOn = true;

            yeouiju.Launched(this.transform.position, z);
            return;
        }

        // predictionLine.enabled = false;
        ReturnYeouiju();
    }

    private void DrawPredictionLine()
    {
        // Draw Prediction Line
        predictionLine.SetPosition(0, this.transform.position);
        predictionHit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, predictionLayerMask);

        if(predictionHit.collider == null)
        {
            predictionLine.enabled = false;
            return;
        }

        // first collision ray
        predictionLine.SetPosition(1, predictionHit.point);

        var inDirection = (predictionHit.point - (Vector2)transform.position).normalized;
        var reflectionDir = Vector2.Reflect(inDirection, predictionHit.normal);

        predictionHit2 = Physics2D.Raycast(predictionHit.point + (reflectionDir * 0.0001f), reflectionDir * 100, Mathf.Infinity, predictionLayerMask);

        if(predictionHit2.collider == null)
            return;
        
        predictionLine.SetPosition(2, predictionHit2.point);

        predictionLine.enabled = true;
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

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
    private Vector2 shouldDrawPoint;
    
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
        FindObjectOfType<MenuButtonManager>().menuButtonEvent += SetLaunchStatus;
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

        // before launch yeouiju, draw prediction line (only in easy mode)
        // if(!isYeouijuOn && prepareYeouiju && usingEasyMode)
        if(!isYeouijuOn && prepareYeouiju)
        {
            DrawPredictionLine();
        }

        if(!Input.GetMouseButtonUp(0))  return;

        // By button up, yeouiju will launched or returned
        if(!isYeouijuOn)
        {
            predictionLine.enabled = false;
            isYeouijuOn = true;

            yeouiju.Launched(this.transform.position, z);
            return;
        }

        ReturnYeouiju();
    }

    /* Draw launch prediction line */
    private void DrawPredictionLine()
    {
        // Draw Prediction Line
        predictionLine.SetPosition(0, this.transform.position);
        predictionHit = Physics2D.Raycast(transform.position, transform.right, Mathf.Infinity, predictionLayerMask);

        if(predictionHit.collider == null)
        {
            // no collision => don't draw prediction line
            predictionLine.enabled = false;
            return;
        }

        // draw first collision point
        shouldDrawPoint = predictionHit.point;
        predictionLine.SetPosition(1, shouldDrawPoint);

        // calculate second ray by Vector2.Reflect
        var inDirection = (predictionHit.point - (Vector2)transform.position).normalized;
        var reflectionDir = Vector2.Reflect(inDirection, predictionHit.normal);

        // By multiply 0.001, can have detail calculation
        predictionHit = Physics2D.Raycast(predictionHit.point + (reflectionDir * 0.001f), reflectionDir, Mathf.Infinity, predictionLayerMask);

        if(predictionHit.collider == null)
        {
            shouldDrawPoint = (Vector2)predictionLine.GetPosition(1) + (reflectionDir * 15f);
        }
        else
        {
            shouldDrawPoint = predictionHit.point;
        }
        
        predictionLine.SetPosition(2, shouldDrawPoint);

        // finally render linerenderer
        predictionLine.enabled = true;
    }

    private void SetYeouijuFalse()
    {
        isYeouijuOn = false;
        prepareYeouiju = false;
    }

    public void SetLaunchStatus(bool isActive)
    {
        StartCoroutine(SetYeouijuStatueCourtine(isActive));
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

    IEnumerator SetYeouijuStatueCourtine(bool isActive)
    {
        yield return null;
        this.canLaunch = isActive;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class PlayerGrapher : MonoBehaviour
{
    // this event = YeouijuLaunch.disJointEvent
    public event Action deleteJointEvent;
    [SerializeField] private float lineModifySpeed;
    private LineRenderer lineRenderer;
    private DistanceJoint2D joint;

    [Header("여의주 상태")]
    private bool nowJoint;
    private bool easyMode;
    [SerializeField] private float minDistance;
    [SerializeField] private float jointMaxTime;
    private float jointTimer;
    [SerializeField] private float canModifyAmount;
    private float nowModify;

    [Header("여의주 HUD")]
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private Image coolTimeImage;
    [SerializeField] private Image modifyAmountImage;


    void Start()
    {
        SetHUDInitial();
        lineModifySpeed *= -1;
        

        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();

        joint.anchor = Vector3.zero;
        SetLine(false);

        FindObjectOfType<YeouijuLaunch>().disJointEvent += DeleteJoint;
        FindObjectOfType<YeouijuReflection>().collisionEvent += MakeJoint;
        
        // if player using easymode, make limit null and don't sub delegate
        easyMode = SaveData.instance.userData.UseEasyMode;
        if(easyMode)
        {
            coolTimeImage = null;
            playerHUD = null;
            modifyAmountImage = null;
            return;
        }
        // another limit is only in hard mode
        FindObjectOfType<YeouijuLaunch>().disJointEvent += SetHUDInitial;
        FindObjectOfType<YeouijuReflection>().collisionEvent += ActiveUI;
    }

    void Update()
    {
        if(!nowJoint)
        {
            SetLine(false);
            jointTimer = jointMaxTime;
            nowModify = 0;
            if(!easyMode)
                SetHUDInitial();
            return;
        }

        if(joint.distance < minDistance)
            deleteJointEvent?.Invoke();
        
        lineRenderer.SetPosition(1, this.transform.position);

        

        // modify grapher line
        float inputModify = Input.GetAxis("Vertical") * lineModifySpeed;
        if(inputModify != 0)
            ModifyLine(inputModify);

        if(easyMode)    return;
        // if game is hard mode, Timer is on
        jointTimer -= Time.deltaTime;
        coolTimeImage.fillAmount = jointTimer / jointMaxTime;

        if(jointTimer < 0)
        {
            nowJoint = false;
            deleteJointEvent?.Invoke();
        }

    }

    void ModifyLine(float amount)
    {
        // if all use modify amount, player can't modify line
        if(nowModify > canModifyAmount)    return;
        
        // modify yeouiju line
        joint.distance += amount;

        if(easyMode)                       return;

        nowModify += Mathf.Abs(amount);

        // set hud 
        modifyAmountImage.fillAmount = (canModifyAmount - nowModify) / canModifyAmount;
    }

    public bool NowJoint()
    {
        return nowJoint;
    }

    public void MakeJoint(Vector2 target)
    {
        nowJoint = true;
        // set joint position
        joint.connectedAnchor = target;
        
        // set line renderer
        lineRenderer.SetPosition(0, target);
        lineRenderer.SetPosition(1, this.transform.position);

        SetLine(true);
    }

    public void ActiveUI(Vector2 dummy)
    {
        playerHUD.SetActive(true);
    }

    public void DeleteJoint()
    {
        nowJoint = false;
        SetLine(false);
    }

    void SetLine(bool active)
    {
        lineRenderer.enabled = active;
        joint.enabled = active;
    }

    private void SetHUDInitial()
    {
        coolTimeImage.fillAmount = 1;
        modifyAmountImage.fillAmount = 1;

        playerHUD.SetActive(false);
    }
}
